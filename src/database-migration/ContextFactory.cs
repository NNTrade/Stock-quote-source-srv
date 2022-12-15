using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using database;
using Microsoft.EntityFrameworkCore;

namespace database_migration;

public class ContextFactory : IDesignTimeDbContextFactory<SQSDbContext>
{
  private IConfigurationBuilder _configurationBuilder;

  public ContextFactory()
  {
    _configurationBuilder = new ConfigurationBuilder();
    _configurationBuilder.AddJsonFile("./appsettings.Development.json");
  }

  public SQSDbContext CreateDbContext(string[] args)
  {
    return CreateDbContext();
  }

  public SQSDbContext CreateDbContext()
  {
    var _config = _configurationBuilder.Build();
    string connectionString = _config.GetConnectionString("DefaultConnection");

    var optionsBuilder = new DbContextOptionsBuilder<SQSDbContext>();
    optionsBuilder.UseNpgsql(connectionString);

    return new SQSDbContext(optionsBuilder.Options);
  }
}
