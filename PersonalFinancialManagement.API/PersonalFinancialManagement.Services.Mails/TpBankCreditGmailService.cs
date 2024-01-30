using System.Globalization;
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

public class TpBankCreditGmailService(
    ILogger<GmailServices> logger,
    IOptions<GoogleCloudSetting> googleCloudSetting
)
    : CreditGmailService,
        ITpBankCreditGmailService
{
    private const string RegexCredit =
        @"<table class=""MsoNormalTable"" border=""0"" cellspacing=""0"" cellpadding=""0"" style='font-family: Helvetica,sans-serif; border-collapse: collapse; mso-table-layout-alt: fixed; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 17.6pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Số thẻ:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border: solid #A3A3A3 1.0pt; border-left: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt; color: #433454'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 1; height: 8.9pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; border-top: none; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 8.9pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Loại thẻ: </span> </p> </td> <td width=""305"" style='width: 228.4pt; border-top: none; border-left: none; border-bottom: solid #A3A3A3 1.0pt; border-right: solid #A3A3A3 1.0pt; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 8.9pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 2; height: 9.5pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; border-top: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 9.5pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Chủ thẻ:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border: solid #A3A3A3 1.0pt; border-left: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 3; height: 9.5pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; border-top: none; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 9.5pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Thời gian thực hiện giao dịch:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border-top: none; border-left: none; border-bottom: solid #A3A3A3 1.0pt; border-right: solid #A3A3A3 1.0pt; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 8.9pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 4; height: 9.5pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; border-top: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 9.5pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Giá trị giao dịch:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border: solid #A3A3A3 1.0pt; border-left: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span><span lang=""VI"" style='margin-left: 5px; font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>VND</span> </p> </td> </tr> <tr style='mso-yfti-irow: 5; height: 8.9pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; border-top: none; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 8.9pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Loại giao dịch:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border-top: none; border-left: none; border-bottom: solid #A3A3A3 1.0pt; border-right: solid #A3A3A3 1.0pt; background: #DFD8E8; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 8.9pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 6; height: 9.5pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Nội dung giao dịch:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border: solid #A3A3A3 1.0pt; border-left: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span> </p> </td> </tr> <tr style='mso-yfti-irow: 4; height: 9.5pt'> <td width=""269"" style='width: 201.65pt; border: solid #A3A3A3 1.0pt;background: #DFD8E8; border-top: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 9.5pt'> <p class=""MsoNormal"" style='line-height: normal'> <span style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>Hạn mức thẻ còn lại:</span> </p> </td> <td width=""305"" style='width: 228.4pt; border: solid #A3A3A3 1.0pt;background: #DFD8E8; border-left: none; padding: 2.0pt 3.0pt 2.0pt 3.0pt; height: 17.6pt'> <p class=""MsoNormal"" style='line-height: normal'> <span lang=""VI"" style='font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>(.*)</span><span lang=""VI"" style='margin-left: 5px; font-size: 11.5pt; font-family: Helvetica,sans-serif; color: #7030A0'>VND</span> </p> </td> </tr> </table>";

    private const string FromEmailFilter = "tpbank@tpb.com.vn";

    private const string TitleFilter =
        "TPBank thông báo giao dịch thẻ quốc tế";

    public const string WalletType = "TPBank Credit";

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
        if (!uIds.Any()) return result;
        foreach (var uid in uIds)
        {
            var message = await inbox.GetMessageAsync(uid);

            var rawTransaction = BuildTransactionRawDataModel(message.HtmlBody);
            if (rawTransaction == null)
                continue;
            rawTransaction.MailId = uid.ToString();
            rawTransaction.WalletType = WalletType;
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
            var changedAmountIndex = 5;
            var changedAmount = res.Groups[changedAmountIndex].Value;
            var cleanedString = RemoveNonNumericCharacters(changedAmount);
            double.TryParse(cleanedString, out var moneyValue);
            var balanceIndex = 8;
            var balance = res.Groups[balanceIndex].Value;
            var cleanedStringBalance = RemoveNonNumericCharacters(balance);
            double.TryParse(cleanedStringBalance, out var balanceValue);
            //var transactionIdIndex = 6;
            var transactionId = "";
            var timeIndex = 4;
            var time = DateTime.ParseExact(res.Groups[timeIndex].Value, "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture);
            var descriptionIndex = 7;
            var description = res.Groups[descriptionIndex].Value;
            var walletIdIndex = 1;
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