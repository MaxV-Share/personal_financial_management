using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.Common.Models.Configurations.Google;

namespace PersonalFinancialManagement.Services.Mails;

public class GmailServices
{
    private readonly GoogleCloudSetting _googleCloudSetting;
    private readonly ILogger<GmailServices> _logger;

    public GmailServices(ILogger<GmailServices> logger,
        IOptions<GoogleCloudSetting> googleCloudSetting)
    {
        _logger = logger;
        _googleCloudSetting = googleCloudSetting.Value;
    }

    public async Task Main()
    {
        _logger.LogInformation($"GmailServices called at {DateTime.Now}");
        // Thay thế các thông tin sau bằng thông tin tài khoản Gmail của bạn
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
        var uids = await inbox.SearchAsync(SearchQuery.DeliveredAfter(new DateTime(2023, 12, 1))
            .And(SearchQuery.FromContains("i2bservices@vpb.com.vn")));

        foreach (var uid in uids)
        {
            var message = await inbox.GetMessageAsync(uid);

            //var creditWalletGoogleModel = ReadCreditVpBank(message.HtmlBody);
            _logger.LogInformation(message.TryParseToString());
            Console.WriteLine("Subject: {0}", message.Subject);
            Console.WriteLine("From: {0}", message.From);
            Console.WriteLine("Body: {0}", message.HtmlBody);

            // Đánh dấu email đã đọc
            //await inbox.AddFlagsAsync(uid, MessageFlags.Draft, true);
        }

        await client.DisconnectAsync(true);
    }
}