using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Castle.Core.Internal;
using PersonalFinancialManagement.Common.Utilities;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.Common.Models.Enums;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.EFCore
{
    internal class Criteria
    {
        public string? Field { get; set; }
        public object? Value { get; set; }
        public FilterType Operator { get; set; }
        public FilterLogicalOperator LogicalOperator { get; set; }
    }

    public static class ExpressionBuilder
    {
        /// <summary>
        /// Builds the filter expression by filter descriptor.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="filterDescriptor">The filter descriptor.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static Expression<Func<TModel, bool>> Build<TModel>(FilterDescriptor? filterDescriptor, string parameterName = "x")
            where TModel : class
        {
            var criteria = GetCriteria(filterDescriptor);

            var parameter = Expression.Parameter(typeof(TModel), parameterName);
            Expression? expression = null;

            if (filterDescriptor != null)
            {
                expression = CreateExpression(parameter, criteria);
            }

            return Expression.Lambda<Func<TModel, bool>>(expression ?? ExpressionUtils.TrueExpression, parameter);
        }

        /// <summary>
        /// Builds the filter expression by filter descriptor.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="filterDescriptors">The filter descriptors.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static Expression<Func<TModel, bool>> Build<TModel>(IEnumerable<FilterDescriptor>? filterDescriptors, string parameterName = "x")
            where TModel : class
        {
            var parameter = Expression.Parameter(typeof(TModel), parameterName);

            Expression? expression = null;
            if (filterDescriptors != null && filterDescriptors.Any())
            {
                foreach (var filterDescriptor in filterDescriptors)
                {
                    var criteria = GetCriteria(filterDescriptor);

                    var innerExpression = CreateExpression(parameter, criteria);
                    if (expression == null)
                    {
                        expression = innerExpression;
                    }
                    else
                    {
                        expression = filterDescriptor.LogicalOperator == FilterLogicalOperator.And ? Expression.AndAlso(expression, innerExpression) : Expression.OrElse(expression, innerExpression);
                    }
                }
            }

            return Expression.Lambda<Func<TModel, bool>>(expression ?? ExpressionUtils.TrueExpression, parameter);
        }

        /// <summary>
        /// Creates the filter expression by filter descriptor.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="filterDecriptor">The filter decriptor.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// parameter
        /// or
        /// filterDecriptor
        /// </exception>
        /// <exception cref="NotSupportedException"></exception>
        private static Expression CreateExpression(Expression parameter, Criteria filterDecriptor)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (filterDecriptor == null)
            {
                throw new ArgumentNullException(nameof(filterDecriptor));
            }

            var member = ExpressionUtils.GetMemberExpression(parameter, filterDecriptor.Field);
            var value = Expression.Constant(filterDecriptor.Value) as Expression;
            var valueType = Nullable.GetUnderlyingType(value.Type) != null ? value.Type : member.Type;
            if (member.Type.IsEnum)
            {
                return CreateExpressionForEnumType(member, value, filterDecriptor);
            }
            switch (filterDecriptor.Operator)
            {
                case FilterType.IsNull:
                    return ExpressionUtils.IsNull(member);

                case FilterType.IsEmpty:
                    return ExpressionUtils.IsEmpty(member);

                case FilterType.IsNotNull:
                    return ExpressionUtils.IsNotNull(member);

                case FilterType.IsNotEmpty:
                    return ExpressionUtils.IsNotEmpty(member);

                case FilterType.IsNullOrWhiteSpace:
                    return ExpressionUtils.IsNullOrWhiteSpace(member);

                case FilterType.IsNotNullOrWhiteSpace:
                    return ExpressionUtils.IsNotNullOrWhiteSpace(member);

                case FilterType.Equal:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsEqual(member, value);

                case FilterType.NotEqual:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsNotEqual(member, value);

                case FilterType.StartsWith:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsStartsWith(member, value);

                case FilterType.EndsWith:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsEndsWith(member, value);

                case FilterType.GreaterThan:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsGreaterThan(member, value);

                case FilterType.GreaterThanOrEqual:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsGreaterThanOrEqual(member, value);

                case FilterType.LessThan:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsLessThan(member, value);

                case FilterType.LessThanOrEqual:
                    value = ExpressionUtils.CreateConstantExpression(filterDecriptor.Value.TryParseToString(), valueType);
                    return ExpressionUtils.IsLessThanOrEqual(member, value);

                case FilterType.Between:
                    var betweenValues = filterDecriptor.Value as string[];
                    var from = ExpressionUtils.CreateConstantExpression(betweenValues[0], valueType);
                    var to = ExpressionUtils.CreateConstantExpression(betweenValues[1], valueType);
                    return ExpressionUtils.IsBetween(member, from, to);

                case FilterType.Contains:
                    var containsValues = filterDecriptor.Value as string[];
                    if (containsValues != null && containsValues.Any())
                    {
                        value = ExpressionUtils.CreateConstantExpression(containsValues[0], valueType);
                        return ExpressionUtils.IsLike(member, value);
                    }
                    value = ExpressionUtils.CreateConstantExpression(containsValues, valueType);
                    return ExpressionUtils.IsContains(member, value);

                case FilterType.NotContains:
                    var notContainsValues = filterDecriptor.Value as string[];
                    if (notContainsValues.Length == 1)
                    {
                        value = ExpressionUtils.CreateConstantExpression(notContainsValues[0], valueType);
                        return ExpressionUtils.IsNotLike(member, value);
                    }
                    value = ExpressionUtils.CreateConstantExpression(notContainsValues, valueType);
                    return ExpressionUtils.IsNotContains(member, value);

                case FilterType.In:
                    var inValues = filterDecriptor.Value as string[];
                    if (member.Type.GetInterfaces().All(t => t != typeof(IList)))
                    {
                        value = ExpressionUtils.CreateConstantExpression(inValues, valueType);
                        return ExpressionUtils.IsIn(member, value);
                    }
                    var inElementType = member.Type.GenericTypeArguments[0]; var containsMethod = member.Type.GetRuntimeMethod("Contains", new[] { inElementType });
                    Expression? inExpression = null;
                    foreach (var inValue in inValues)
                    {
                        var containValue = ExpressionUtils.CreateConstantExpression(inValue, inElementType); var innerExpression = Expression.Call(member, containsMethod, containValue);

                        if (inExpression == null)
                        { inExpression = innerExpression; }
                        else { inExpression = Expression.OrElse(inExpression, innerExpression); }
                    }
                    return inExpression;

                case FilterType.NotIn:
                    var notInValues = filterDecriptor.Value as string[]; if (member.Type.GetInterfaces().All(t => t != typeof(IList)))
                    {
                        value = ExpressionUtils.CreateConstantExpression(notInValues, valueType); return ExpressionUtils.IsNotIn(member, value);
                    }
                    var notInElementType = member.Type.GenericTypeArguments[0]; var notInContainsMethod = member.Type.GetRuntimeMethod("Contains", new[] { notInElementType });

                    Expression? notInExpression = null;
                    foreach (var notInValue in notInValues)
                    {
                        var notContainValue = ExpressionUtils.CreateConstantExpression(notInValue, notInElementType); var innerExpression = Expression.Call(member, notInContainsMethod, notContainValue);

                        if (notInExpression == null)
                        {
                            notInExpression = innerExpression;
                        }
                        else
                        {
                            notInExpression = Expression.AndAlso(notInExpression, innerExpression);
                        }
                    }
                    return Expression.Not(notInExpression);
            }
            throw new NotSupportedException($"{filterDecriptor.Operator.ToString()} is not supported by {value.Type} ");
        }

        /// <summary>
        /// Creates the type of the expression for enum.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="value">The value.</param>
        /// <param name="filterDecriptor">The filter decriptor.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">'{operationType}' is not supported by '{value.Type}'</exception>
        private static Expression CreateExpressionForEnumType(Expression member, Expression value, Criteria filterDecriptor)
        {
            var expression = Expression.Convert(member, Enum.GetUnderlyingType(member.Type));
            value = Expression.Convert(value, Enum.GetUnderlyingType(value.Type));

            switch (filterDecriptor.Operator)
            {
                case FilterType.In:
                case FilterType.Contains:
                    return Expression.Equal(Expression.Or(expression, value), value);

                case FilterType.NotIn:
                case FilterType.NotContains:
                    return Expression.Not(Expression.Equal(Expression.Or(expression, value), value));

                case FilterType.Equal:
                    return Expression.Equal(expression, value);

                case FilterType.NotEqual:
                    return Expression.NotEqual(expression, value);
            }

            throw new NotSupportedException($"{filterDecriptor.Operator.ToString()} is not supported by {value.Type} ");
        }

        /// <summary>
        /// Throws if invalid.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <exception cref="ArgumentNullException">filter</exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static Criteria GetCriteria(FilterDescriptor filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var values = new List<string>();
            if (filter.Values is { Length: > 0 })
            {
                values.AddRange(filter.Values.Select(item => item.Nullify()).Where(value => !value.IsNullOrEmpty()));
            }

            filter.Values = values.ToArray();

            switch (filter.Operator)
            {
                case FilterType.Equal:
                case FilterType.NotEqual:
                case FilterType.StartsWith:
                case FilterType.EndsWith:
                case FilterType.GreaterThan:
                case FilterType.GreaterThanOrEqual:
                case FilterType.LessThan:
                case FilterType.LessThanOrEqual:
                    if (filter.Values.Length != 1)
                    {
                        throw new ArgumentException();
                    }

                    return new Criteria
                    {
                        Field = filter.Field,
                        LogicalOperator = filter.LogicalOperator,
                        Operator = filter.Operator,
                        Value = filter.Values[0]
                    };

                case FilterType.Contains:
                case FilterType.NotContains:
                    if (filter.Values.Length == 0)
                    {
                        throw new ArgumentException();
                    }

                    return new Criteria
                    {
                        Field = filter.Field,
                        LogicalOperator = filter.LogicalOperator,
                        Operator = filter.Operator,
                        Value = filter.Values
                    };

                case FilterType.Between:
                    if (filter.Values.Length != 2)
                    {
                        throw new ArgumentException();
                    }

                    return new Criteria
                    {
                        Field = filter.Field,
                        LogicalOperator = filter.LogicalOperator,
                        Operator = filter.Operator,
                        Value = new[] { filter.Values[0], filter.Values[1] }
                    };

                case FilterType.IsNull:
                case FilterType.IsEmpty:
                case FilterType.IsNotNull:
                case FilterType.IsNotEmpty:
                case FilterType.IsNullOrWhiteSpace:
                case FilterType.IsNotNullOrWhiteSpace:
                    if (filter.Values.Length != 0)
                    {
                        throw new ArgumentException();
                    }

                    return new Criteria
                    {
                        Field = filter.Field,
                        LogicalOperator = filter.LogicalOperator,
                        Operator = filter.Operator,
                        Value = null
                    };

                case FilterType.In:
                case FilterType.NotIn:
                    if (filter.Values.Length == 0)
                    {
                        throw new ArgumentException();
                    }

                    return new Criteria
                    {
                        Field = filter.Field,
                        LogicalOperator = filter.LogicalOperator,
                        Operator = filter.Operator,
                        Value = filter.Values
                    };
                case FilterType.NotBetween:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new ArgumentException();
        }
    }
}
