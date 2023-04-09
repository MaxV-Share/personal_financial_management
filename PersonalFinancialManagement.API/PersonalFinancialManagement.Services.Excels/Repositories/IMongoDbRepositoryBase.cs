using MongoDB.Driver;

namespace PersonalFinancialManagement.Services.Excels.Repositories;

public interface IMongoDbRepositoryBase<T> where T : MongoEntity
{
    IMongoCollection<T> FindAll(ReadPreference? readPreference = null);
    Task CreateAsync(T entity);
    Task CreateAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}
