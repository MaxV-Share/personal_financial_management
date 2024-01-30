﻿using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalFinancialManagement.Common.Models.Configurations.Google;
using PersonalFinancialManagement.Models.Dtos.Google;
using PersonalFinancialManagement.Services.Mails.Interfaces;

namespace PersonalFinancialManagement.Services.Mails;

public class VpBankCreditGmailService(
    ILogger<GmailServices> logger,
    IOptions<GoogleCloudSetting> googleCloudSetting)
    : CreditGmailService, IVpBankCreditGmailService
{
    private const string RegexCredit =
        @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left""> <tr style=""border-collapse:collapse""> <td class=""es-m-p20b"" align=""left"" style=""padding:0;Margin:0;width:280px""> <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px""> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px;color:#ff0000""><b>(.*) <\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Số tiền thay đổi \/ <span style=""font-size:12px""><em>Changed Amount<\/em><\/span><\/p><\/td> <\/tr> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px""><b>(.*)<\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Nội dung \/ <span style=""font-size:12px""><em>Transaction Content<\/em><\/span><\/p><\/td> <\/tr> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px""><b>(.*)<\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Thời gian&nbsp;\/<span style=""font-size:12px""><em>Time<\/em><\/span><\/p><\/td> <\/tr> <\/table><\/td> <\/tr> <\/table><!--.*--> <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right""> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:0;Margin:0;width:275px""> <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px""> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px;color:#008000""><b>(.*) VND <\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Hạn mức còn lại \/ <span style=""font-size:12px""><em>Available Limit<\/em><\/span><\/p><\/td> <\/tr> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px""><b>(.*)<\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Thẻ \/<span style=""font-size:12px""><em> Card<\/em><\/span><\/p><\/td> <\/tr> <tr style=""border-collapse:collapse""> <td align=""left"" style=""padding:5px;Margin:0""><h5 style=""Margin:0;line-height:17px;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;font-size:14px""><b>(.*)<\/b><\/h5><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:20px;color:#333333;font-size:13px"">Mã giao dịch \/ <span style=""font-size:12px""><em>Transaction Code<\/em><\/span><\/p><\/td> <\/tr> <\/table><\/td> <\/tr> <\/table>";

    private const string FromEmailFilter = "i2bservices@vpb.com.vn";

    private const string TitleFilter =
        "VPBank xin thong bao bien dong so du The tin dung cua Quy khach";

    private readonly GoogleCloudSetting _googleCloudSetting = googleCloudSetting.Value;

    public async Task<List<RawTransactionViewModel>> GetCreditWalletGoogles(
        DateTime? fromDateTime = null,
        List<string>? oldUId = null)
    {
        logger.LogInformation($"GmailServices called at {DateTime.Now}");

        var email = _googleCloudSetting.GmailAccountSetting.Username;
        var password = _googleCloudSetting.GmailAccountSetting.Password;

        using var client = new ImapClient();

        await client.ConnectAsync("imap.gmail.com", 993, true);

        // Xác thực với tên người dùng và mật khẩu
        await client.AuthenticateAsync(email, password);

        // Mở thư mục Inbox
        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        // Tìm các email mới
        var query = SearchQuery.FromContains(FromEmailFilter).And(
            SearchQuery.SubjectContains(
                TitleFilter));
        if (fromDateTime != null)
            query = SearchQuery.DeliveredAfter(fromDateTime.Value)
                .And(query);
        var uIds = await inbox.SearchAsync(query);
        var result = new List<RawTransactionViewModel>();
        if (oldUId != null)
            uIds = uIds.Where(e => !oldUId.Contains(e.ToString())).ToList();
        if (!uIds.Any()) return null;
        foreach (var uid in uIds)
        {
            var message = await inbox.GetMessageAsync(uid);

            var rawTransaction = BuildTransactionRawDataModel(message.HtmlBody);
            if (rawTransaction == null)
                continue;
            rawTransaction.MailId = uid.ToString();
            result.Add(rawTransaction);
        }

        await client.DisconnectAsync(true);
        return result;
    }

    protected override RawTransactionViewModel? BuildTransactionRawDataModel(string html)
    {
        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var res = Regex.Match(Regex.Replace(html, @"\s{2,}|\n", " "),
                Regex.Replace(RegexCredit, @"\s{2,}|\n", " "));
            // Lấy thông tin theo nhãn (label)
            var changedAmountIndex = 1;
            var changedAmount = res.Groups[changedAmountIndex].Value;
            var cleanedString = RemoveNonNumericCharacters(changedAmount);
            double.TryParse(cleanedString, out var moneyValue);
            var balanceIndex = 4;
            var balance = res.Groups[balanceIndex].Value;
            var cleanedStringBalance = RemoveNonNumericCharacters(balance);
            double.TryParse(cleanedStringBalance, out var balanceValue);
            var transactionIdIndex = 6;
            var transactionId = res.Groups[transactionIdIndex].Value;
            var timeIndex = 3;
            var time = DateTime.ParseExact(res.Groups[timeIndex].Value, "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture);
            var descriptionIndex = 2;
            var description = res.Groups[descriptionIndex].Value;
            var walletIdIndex = 5;
            var walletId = res.Groups[walletIdIndex].Value;
            var result = new RawTransactionViewModel
            {
                TransactionDate = time,
                TransactionId = transactionId,
                Amount = moneyValue,
                Balance = balanceValue,
                Description = description,
                ReferenceCode = "",
                WalletId = walletId
            };
            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }

        return null;
    }

    private string RemoveNonNumericCharacters(string input)
    {
        // Loại bỏ các ký tự không phải số, dấu phẩy, và dấu trừ
        var cleanedString =
            new string(input.Where(c => char.IsDigit(c) || c == ',' || c == '-' || c == '.')
                .ToArray());
        return cleanedString;
    }
}