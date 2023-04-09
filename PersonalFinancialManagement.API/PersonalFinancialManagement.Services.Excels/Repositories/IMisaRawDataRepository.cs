using PersonalFinancialManagement.Services.Excels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Services.Excels.Repositories
{
    public interface IMisaRawDataRepository : IMongoDbRepositoryBase<MisaRawDataEntry>
    {
    }
}
