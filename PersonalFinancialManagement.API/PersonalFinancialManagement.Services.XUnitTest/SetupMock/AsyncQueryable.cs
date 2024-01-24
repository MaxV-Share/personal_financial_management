using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace PersonalFinancialManagement.Services.XUnitTest.SetupMock;

public static class AsyncQueryable
{
    /// <summary>
    ///     Returns the input typed as IQueryable that can be queried asynchronously
    /// </summary>
    /// <typeparam name="TEntity">The item type</typeparam>
    /// <param name="source">The input</param>
    public static IQueryable<TEntity> AsAsyncQueryable<TEntity>(this IEnumerable<TEntity> source)
    {
        return new AsyncQueryable<TEntity>(source ??
                                           throw new ArgumentNullException(nameof(source)));
    }
}

public class AsyncQueryable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>,
    IQueryable<TEntity>
{
    public AsyncQueryable(IEnumerable<TEntity> enumerable) : base(enumerable)
    {
    }

    public AsyncQueryable(Expression expression) : base(expression)
    {
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return new AsyncEnumerator(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new AsyncQueryProvider(this);

    public IAsyncEnumerator<TEntity> GetEnumerator()
    {
        return new AsyncEnumerator(this.AsEnumerable().GetEnumerator());
    }

    private class AsyncEnumerator : IAsyncEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> inner;

        public AsyncEnumerator(IEnumerator<TEntity> inner)
        {
            this.inner = inner;
        }

        public TEntity Current => inner.Current;
        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(inner.MoveNext());
        }
#pragma warning disable CS1998 // Nothing to await
        public async ValueTask DisposeAsync()
        {
            inner.Dispose();
        }
#pragma warning restore CS1998
        public void Dispose()
        {
            inner.Dispose();
        }
    }

    private class AsyncQueryProvider : IAsyncQueryProvider
    {
        private readonly IQueryProvider inner;

        internal AsyncQueryProvider(IQueryProvider inner)
        {
            this.inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncQueryable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncQueryable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return inner.Execute<TResult>(expression);
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression,
            CancellationToken cancellationToken)
        {
            return Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new AsyncQueryable<TResult>(expression);
        }
    }
}