using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.Models.DbContexts;

namespace PersonalFinancialManagement.Repositories.XUnitTest;

public class InMemoryDbContextFactory
{
    public ApplicationDbContext GetApplicationDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryApplicationDatabase")
            .Options;
        var dbContext = new ApplicationDbContext(options);

        return dbContext;
    }
}