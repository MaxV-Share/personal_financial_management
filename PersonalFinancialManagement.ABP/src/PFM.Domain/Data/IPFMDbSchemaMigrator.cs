using System.Threading.Tasks;

namespace PFM.Data;

public interface IPFMDbSchemaMigrator
{
    Task MigrateAsync();
}
