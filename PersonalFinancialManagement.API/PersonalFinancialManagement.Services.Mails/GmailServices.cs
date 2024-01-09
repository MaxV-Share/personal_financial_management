using Google.Apis.Gmail.v1;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Logging;

namespace PersonalFinancialManagement.Services.Mails;

public class GmailServices
{
    private static string[] Scopes = { GmailService.Scope.GmailReadonly };
    private static readonly string ApplicationName = "Gmail API .NET Quickstart";
    private readonly ILogger<GmailServices> _logger;

    public GmailServices(ILogger<GmailServices> logger)
    {
        _logger = logger;
    }

    public async Task Main()
    {
        _logger.LogInformation($"GmailServices called at {DateTime.Now}");
        // Thay thế các thông tin sau bằng thông tin tài khoản Gmail của bạn
        var email = "thevinh.it.kh.1997@gmail.com";
        var password = "zqnn pshe xibq qend";

        using var client = new ImapClient();

        await client.ConnectAsync("imap.gmail.com", 993, true);

        // Xác thực với tên người dùng và mật khẩu
        await client.AuthenticateAsync(email, password);

        // Mở thư mục Inbox
        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        // Tìm các email mới
        var uids = await inbox.SearchAsync(SearchQuery.DeliveredAfter(new DateTime(2023, 12, 1))
            .And(SearchQuery.FromContains("vpbankonline@vpb.com.vn")));

        foreach (var uid in uids)
        {
            var message = await inbox.GetMessageAsync(uid);

            Console.WriteLine("Subject: {0}", message.Subject);
            Console.WriteLine("From: {0}", message.From);
            Console.WriteLine("Body: {0}", message.TextBody);

            // Đánh dấu email đã đọc
            await inbox.AddFlagsAsync(uid, MessageFlags.Draft, true);
        }

        await client.DisconnectAsync(true);
    }
}