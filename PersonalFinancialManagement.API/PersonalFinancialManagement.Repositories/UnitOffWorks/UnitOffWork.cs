using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Repositories.BaseRepository;
using PersonalFinancialManagement.Models.Entities.Identities;
using Microsoft.Data.SqlClient;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Repositories.UnitOffWorks
{
    public class UnitOffWork<TContext> : IUnitOffWork<TContext> where TContext: DbContext
    {
        private readonly TContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private Dictionary<Type, object?>? _repositories;
        private UserManager<User>? _userManager;

        public UnitOffWork(TContext context, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _serviceProvider = serviceProvider;
        }

        //~UnitOffWork()
        //{
        //    Dispose(false);
        //}

        //public virtual void Dispose(bool disposing)
        //{
        //    if (_disposed)
        //    {
        //        return;
        //    }

        //    if (disposing)
        //    {
        //        _tx?.Dispose();
        //        _tx = null;
        //    }

        //    _disposed = true;
        //}
        //void IDisposable.Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task DoWorkWithTransaction(Action action)
        {
            await using var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                action.Invoke();
                await trans.CommitAsync();
            }
            catch
            {
                await trans.RollbackAsync();
                throw;
            }
        }

        public async Task DoWorkWithTransaction(Task<Action> action)
        {
            using (var trans = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    (await action).Invoke();
                    await trans.CommitAsync();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public async Task<T> DoWorkWithTransaction<T>(Func<Task<T>> action)
        {
            await using var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await action.Invoke();
                await trans.CommitAsync();
                return result;
            }
            catch
            {
                await trans.RollbackAsync();
                throw;
            }
        }

        //public async Task<T> DoWorkWithTransaction<T>(Func<T> action)
        //{
        //    using (var trans = await _dbContext.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            var result = action.Invoke();
        //            await trans.CommitAsync();
        //            return result;
        //        }
        //        catch
        //        {
        //            trans.Rollback();
        //            throw;
        //        }
        //    }
        //}

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string query)
        {
            await using var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString);
            connection.Open();

            var result = await connection.QueryAsync<TResult>(query);
            return result;
        }

        public IBaseRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            _repositories ??= new Dictionary<Type, object?>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = _serviceProvider.GetService<IBaseRepository<TEntity, TKey>>();
            return _repositories[type] as IBaseRepository<TEntity, TKey> ?? throw new InvalidOperationException();
        }

        public UserManager<User>? UserManager => _userManager ??= _serviceProvider.GetService<UserManager<User>>();

        public string ToQueryString(IQueryable queryable)
        {
            var a = queryable.ToQueryString();
            return "";
        }
    }
}
