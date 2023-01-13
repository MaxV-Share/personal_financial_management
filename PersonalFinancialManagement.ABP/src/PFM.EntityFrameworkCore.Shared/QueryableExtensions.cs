using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFM.EntityFrameworkCore.Shared;
using PFM.Common.Models;
using PFM.Extensions;
using PFM.Common.Models.Enums;
using System.Linq.Expressions;
using PFM.Common.Models.DTOs;
using Volo.Abp.Application.Dtos;
using System.Linq.Dynamic.Core;

namespace PFM.EntityFrameworkCore.Shared
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, FilterRequest filter) where T : class
        {
            if (filter == null || filter.Details.ListIsNullOrEmpty())
            {
                return source;
            }
            var fields = typeof(T).GetProperties();
            var filters = filter.Details.Where(d => fields.Any(f => string.Equals(d.AttributeName, f.Name, StringComparison.OrdinalIgnoreCase))).Select(e => new FilterDescriptor()
            {
                Field = e.AttributeName,
                Values = e.FilterType == FilterType.In ? e.Value.Split("|") : new string[] { e.Value },
                LogicalOperator = filter.LogicalOperator,
                Operator = e.FilterType,
            }).ToList();
            return source.Where(ExpressionBuilder.Build<T>(filters));
        }

        /// <summary> 
        /// Determines whether this instance is ordered. 
        /// </summary>
        /// <typeparam name = "T" ></ typeparam >
        /// <param name="source">The queryable.</param> 
        /// <returns 
        /// <c>true</c> if the specified queryable is ordered; otherwise, <c>false</c>. 
        /// </returns> 
        /// <exception cref="ArgumentNullException">queryable</exception> O references 
        public static bool IsOrdered<T>(this IQueryable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return source.Expression.Type.IsAssignableFrom(typeof(IOrderedQueryable<T>));
        }

        #region Where
        /// <summary> 
        /// Filters data in the given source with filter descriptor. 
        /// </summary> WII <typeparam name="T"></typeparam> 
        /// <param name="source">The source.</param> WII <param name="filter">The filter.</param 
        /// <param name="parameterName">Name of the parameter.</param> 
        /// <returns X/returns 2 references 
        public static IQueryable<T> Where<T>(this IQueryable<T> source, FilterDescriptor filter, string parameterName = "x")
        where T : class
        {
            var expression = ExpressionBuilder.Build<T>(filter, parameterName);
            if (expression == null)
                return source;
#if AS_DEBUG
            System.Diagnostics.Debug.WriteLine("Filter Expression: " + expression.Body.ToString());
#endif
            return source.Where(expression);
        }

        /// <summary> 
        /// Filters data in the given source with filter descriptors. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="source">The source.</param> W <param name="filters">The filters.</param> 
        /// <param name="parameterName">Name of the parameter.</param> 
        /// <returns X/returns 4 references 
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IEnumerable<FilterDescriptor> filters, string parameterName = "x")
        where T : class
        {
            var expression = ExpressionBuilder.Build<T>(filters, parameterName);
            if (expression == null)
                return source;
#if AS_DEBUG
            System.Diagnostics.Debug.WriteLine("Filter Expression: " + expression.Body.ToString();
#endif
            return source.Where(expression);
        }

        #endregion

        #region OrderBy

        /// <summary> 
        /// Orders data in the given source with sort descriptor. 
        /// </summary> VII <typeparam name="T"></typeparam>, 
        /// <param name="source">The source.</param> 
        /// <param name="sort">The sort.</param>
        /// <param name = "replaceOrder" >if set to<c>true</c> [replace the current order in source].</param> 
        /// <returns X/returns O references 
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortDescriptor sort, bool replaceOrder = true)
        where T : class
        {
            if (source == null || sort == null)
                return source;
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, sort.Field);
            return source.OrderBy(property, parameter, sort.Direction, replaceOrder);
        }

        /// <summary> 
        /// Orders data in the given source with sort descriptors. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="source">The source.</param> MI <param name="sorts">The sorts.</param>
        /// <param name = "replaceOrder" >if set to<c>true</c> [replace the current order in source].</param)
        /// <returns X/returns> 5 references 
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortDescriptor> sorts, bool replaceOrder = true)
        where T : class
        {
            if (source.ListIsNullOrEmpty() || sorts.ListIsNullOrEmpty())
            {
                return source;
            }
            var parameter = Expression.Parameter(typeof(T));
            foreach (var item in sorts)
            {
                var property = Expression.Property(parameter, item.Field);
                source = source.OrderBy(property, parameter, item.Direction, replaceOrder);
                replaceOrder = false;
            }
            return source;
        }

        /// <summary> 
        /// Orders data in the given source. 
        /// </summary>
        /// <typeparam name="T"></typeparam> MI <param name="source">The source.</param> 
        /// <param name="property">The property.</param>
        /// <param name="parameter">The parameter.</param> 
        /// <param name="direction">The direction.</param>
        /// <param name = "replaceOrder" >if set to<c>true</c> [replace the current order in source].</param> 
        /// <returns X/returns 2 references 
        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, MemberExpression property, ParameterExpression parameter, SortDirection direction, bool replaceOrder = true)
        where T : class
        {
            var propertyType = property.Type;
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            var isNullable = underlyingType != null;
            if (propertyType == typeof(string))
            {
                return source.OrderBy<T, string>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(DateTime) || isNullable && underlyingType == typeof(DateTime))
            {
                if (isNullable)
                    return source.OrderBy<T, DateTime?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, DateTime>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(int) || isNullable && underlyingType == typeof(int))
            {
                if (isNullable)
                    return source.OrderBy<T, int?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, int>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(long) || isNullable && underlyingType == typeof(long))
            {
                if (isNullable)
                    return source.OrderBy<T, long?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, long>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(bool) || isNullable && underlyingType == typeof(bool))
            {
                if (isNullable)
                    return source.OrderBy<T, bool?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, bool>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(Guid) || isNullable && underlyingType == typeof(Guid))
            {
                if (isNullable)
                    return source.OrderBy<T, Guid?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, Guid>(property, parameter, direction, replaceOrder);
            }

            if (propertyType == typeof(TimeSpan) || isNullable && underlyingType == typeof(TimeSpan))
            {
                if (isNullable)
                    return source.OrderBy<T, TimeSpan?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, TimeSpan>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(DateTimeOffset) || isNullable && underlyingType == typeof(DateTimeOffset))
            {
                if (isNullable)
                    return source.OrderBy<T, DateTimeOffset?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, DateTimeOffset>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(byte) || isNullable && underlyingType == typeof(byte))
            {
                if (isNullable)
                    return source.OrderBy<T, byte?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, byte>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(float) || isNullable && underlyingType == typeof(float))
            {
                if (isNullable)
                    return source.OrderBy<T, float?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, float>(property, parameter, direction, replaceOrder);
            }

            if (propertyType == typeof(double) || isNullable && underlyingType == typeof(double))
            {
                if (isNullable)
                    return source.OrderBy<T, double?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, double>(property, parameter, direction, replaceOrder);
            }
            if (propertyType == typeof(decimal) || isNullable && underlyingType == typeof(decimal))
            {
                if (isNullable)
                    return source.OrderBy<T, decimal?>(property, parameter, direction, replaceOrder);
                return source.OrderBy<T, decimal>(property, parameter, direction, replaceOrder);
            }
            return source.OrderBy<T, dynamic>(property, parameter, direction, replaceOrder);
        }

        /// <summary> 
        /// Orders data in the given source. 
        /// </summary> 777 <typeparam name="T"X</typeparam> 
        /// <typeparam name="TProperty">The type of the property.</typeparam> 
        /// <param name="source">The source.</param> W <param name="property">The property.</param> 
        /// <param name="parameter">The parameter.</param> III <param name="direction">The direction.</param>
        /// <param name="replaceOrder">if set to <c>true</c> [replace the current order in source.</param> 
        /// <returns></returns> 24 references 
        private static IOrderedQueryable<T> OrderBy<T, TProperty>(this IQueryable<T> source, MemberExpression property, ParameterExpression parameter, SortDirection direction, bool replaceOrder = true)
        {
            var expression = Expression.Lambda<Func<T, TProperty>>(property, parameter);
            if (replaceOrder || !source.Expression.Type.IsAssignableFrom(typeof(IOrderedQueryable<T>)) || !(source is IOrderedQueryable<T> orderedQueryable))
                return direction == SortDirection.Asc ? source.OrderBy(expression) : source.OrderByDescending(expression);
            return direction == SortDirection.Asc ? orderedQueryable.ThenBy(expression) : orderedQueryable.ThenByDescending(expression);
        }
        #endregion

        #region Paging
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Task<BasePaging<TSource>> ToPagingAsync<TSource>(this IQueryable<TSource> queryable, Pagination pagination)
        {
            if (pagination == null)
            {
                throw new ArgumentNullException(nameof(pagination));
            }
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }
            return PagingAsync(queryable, pagination);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<BasePaging<TSource>> ToPagingAsync<TSource>(this IQueryable<TSource> queryable, IFilterBodyRequest request)
            where TSource : class
        {

            if (request.Filter != null && !request.Filter.Details.ListIsNullOrEmpty())
                queryable = queryable.Filter(request.Filter);

            if (!request.Orders.ListIsNullOrEmpty())
                queryable = queryable.OrderBy(request.Orders, true);

            if (request.Pagination == null)
            {
                request.Pagination = new Pagination();
            }
            var result = await queryable.ToPagingAsync(request.Pagination);

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="request"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<BasePaging<TResult>> ToPagingAsync<TResult, TSource>(this IQueryable<TSource> queryable, IFilterBodyRequest request, Expression<Func<TSource, TResult>> selector) where TResult : class
        {
            if (request == null || request.Pagination == null)
                throw new ArgumentNullException(nameof(request));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return PagingAsync(queryable, request, selector);
        }
        private static async Task<BasePaging<TResult>> PagingAsync<TResult, TSource>(IQueryable<TSource> queryable, IFilterBodyRequest request, Expression<Func<TSource, TResult>> selector) where TResult : class
        {
            var ViewModelQuery = queryable.Select(selector);

            var result = await ViewModelQuery.ToPagingAsync(request);

            return result;
        }    

        private static async Task<BasePaging<T>> PagingAsync<T>(IQueryable<T> queryable, Pagination pagination)
        {
            var result = new BasePaging<T>();
            var totalRow = await queryable.CountAsync();
            result.Items = await queryable
                                   .Skip(pagination.SkipCount)
                                   .Take(pagination.MaxResultCount)
                                   .ToListAsync();
            result.Pagination = pagination;
            result.TotalCount = totalRow;
            return result;
        }
        #endregion
    }
}
