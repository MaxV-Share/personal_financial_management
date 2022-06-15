using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalFinancialManagement.Repositories.BaseRepository;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities.Identities;
using System.Data.SqlClient;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Repositories.UnitOffWorks
{
    public class UnitOffWork : IUnitOffWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed = false;
        private readonly IServiceProvider _serviceProvider;
        private Dictionary<Type, object> _repositories;
        private IDbContextTransaction _tx { get; set; }
        private UserManager<User> _userManager;

        public UnitOffWork(ApplicationDbContext context, IServiceProvider serviceProvider)
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
            using (var trans = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    action.Invoke();
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
            using (var trans = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await action.Invoke();
                    await trans.CommitAsync();
                    return result;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
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
            IEnumerable<TResult> result;
            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                result = await connection.QueryAsync<TResult>(query);
            }
            return result;
        }

        public IBaseRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = _serviceProvider.GetService<IBaseRepository<TEntity, TKey>>();
            }


            return (IBaseRepository<TEntity, TKey>)_repositories[type];
        }

        public UserManager<User> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = _serviceProvider.GetService<UserManager<User>>();
                }
                return _userManager;
            }
        }
    }
}
