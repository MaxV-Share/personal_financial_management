using PersonalFinancialManagement.Services.Excels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using PersonalFinancialManagement.Services.Excels.Configurations;

namespace PersonalFinancialManagement.Services.Excels.Repositories
{
    public class MisaRawDataRepository : MongoDbRepository<MisaRawDataEntry>, IMisaRawDataRepository
    {
        public MisaRawDataRepository(IMongoClient client, MongoDbSettings settings) : base(client, settings)
        {
        }
    }
}
