using HtmlAgilityPack;
using PersonalFinancialManagement.GoogleServices.Models;

namespace PersonalFinancialManagement.Services.Mails;

public abstract class CreditGmailService
{
    protected abstract CreditWalletGoogleModel? BuildCreditWalletGoogleModel(string html);
    protected abstract string GetValueFromHtml(HtmlDocument doc, string label);
}