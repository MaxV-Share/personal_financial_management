using Microsoft.Extensions.Configuration;

namespace PersonalFinancialManagement.Services.Excels.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetOptions<T>(this IConfiguration configuration, string sectionName)
        where T : new()
    {
        return configuration.GetSection(sectionName).Get<T>();
    }
}