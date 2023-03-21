using System.ComponentModel;

namespace PersonalFinancialManagement.Common
{
    public static class ObjectHelper
    {
        public static string? GetDescriptionAttributeValue<T>(this T value, string propertyName)
        {
            var type = typeof(T).GetProperty(propertyName);
            var attribute = type.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            if (attribute == null)
                return null;
            var description = (DescriptionAttribute)attribute;
            var result = description.Description;
            return result;
        }
    }
}
