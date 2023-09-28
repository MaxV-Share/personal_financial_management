using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Services.Base
{
    public interface IBaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey>
        where TEntity : BaseEntity<TKey>, new()
        where TCreateRequest : BaseCreateRequest, new()
        where TUpdateRequest : BaseUpdateRequest<TKey>, new()
        where TViewModel : BaseViewModel<TKey>, new()
        where TContext: DbContext
    {
        Task<IEnumerable<TViewModel>?> GetAllDTOAsync();
        Task<TViewModel?> GetByIdAsync(TKey id);
        Task<int?> DeleteHardAsync(TKey id);
        Task<int?> DeleteSoftAsync(TKey id);
        Task<TViewModel?> UpdateAsync(TKey id, TUpdateRequest request);
        Task<TViewModel?> CreateAsync(TCreateRequest request);
        Task<IEnumerable<TViewModel>?> CreateAsync(IEnumerable<TCreateRequest> request);
    }
}
