using System.Data.Common;
using database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Xunit.Abstractions;

namespace service_test.tool;

public abstract class BaseTest : IDisposable
{
  protected readonly ITestOutputHelper output;
  protected DbContextOptionsBuilder _optionsBuilder;

  private static DbContextOptionsBuilder GetOptionBuilder(string dbSuffix)
  {
    var _configurationBuilder = new ConfigurationBuilder();
    _configurationBuilder.AddJsonFile("./appsettings.test.json");

    var _config = _configurationBuilder.Build();
    DbConnectionStringBuilder _connectionStringBuilder =
        new NpgsqlConnectionStringBuilder(_config.GetConnectionString("DefaultConnection"));
    _connectionStringBuilder["Database"] += dbSuffix + DateTimeOffset.UtcNow;

    var _optionsBuilder = new DbContextOptionsBuilder();

    _optionsBuilder.UseNpgsql(_connectionStringBuilder.ConnectionString);
    return _optionsBuilder;
  }

  public BaseTest(string dbSuffix, ITestOutputHelper output)
  {
    this.output = output;
    _optionsBuilder = GetOptionBuilder(dbSuffix);
    using (var _context = new SQSDbContext(_optionsBuilder.Options))
    {
      _context.Database.EnsureDeleted();
      _context.Database.Migrate();
    }
  }

  protected SQSDbContext CreateContext()
  {
    return new SQSDbContext(_optionsBuilder.Options);
  }
  public virtual void Dispose()
  {
    using (var _context = new SQSDbContext(_optionsBuilder.Options))
    {
      _context.Database.EnsureDeleted();
    }
  }
}
