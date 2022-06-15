using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Common.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks if the provided collection is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (typeof(T) is string)
            {
                return string.IsNullOrWhiteSpace(source as string);
            }

            return source == null || !source.Any();
        }
    }
}
