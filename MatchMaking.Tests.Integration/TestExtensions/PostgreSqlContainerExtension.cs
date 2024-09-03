using MatchMaking.Database;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace MatchMaking.Tests.Integration.TestExtensions;

public static class PostgreSqlContainerExtension
{
    public static ApplicationDbContext ToDbContext(this PostgreSqlContainer container)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(container.GetConnectionString())
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();

        return context;
    }
}