using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalFinancialManagement.Services.Excels.Configurations;
using PersonalFinancialManagement.Services.Excels.Entities;

namespace PersonalFinancialManagement.Services.Excels.Repositories;

public class MisaRawDataRepository : MongoDbRepository<MisaRawDataEntry>, IMisaRawDataRepository
{
    public MisaRawDataRepository(IMongoClient client, IOptions<MongoDbSettings> settings) : base(
        client, settings.Value)
    {
    }
}