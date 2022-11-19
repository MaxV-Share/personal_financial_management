using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PersonalFinancialManagement.Repositories.BaseRepository;
using PersonalFinancialManagement.Models.Entities.Identities;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Repositories.UnitOffWorks
{
    public interface IUnitOffWork<TContext> //: IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task DoWorkWithTransaction(Action action);
        //Task<T> DoWorkWithTransaction<T>(Func<T> action);
        Task<T> DoWorkWithTransaction<T>(Func<Task<T>> action);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string query);
        IBaseRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        UserManager<User> UserManager { get; }
    }
}
