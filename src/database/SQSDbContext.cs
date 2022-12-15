using database.Entity;
using Microsoft.EntityFrameworkCore;

namespace database;
public class SQSDbContext : DbContext
{
  public SQSDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<Quote> Quotes { get; set; }
  public DbSet<Source> Sources { get; set; }
  public DbSet<Stock> Stocks { get; set; }
  public DbSet<StockSourceMap> StockSourceMaps { get; set; }
  public DbSet<TimeFrame> TimeFrames { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    TimeFrame.OnModelCreating(modelBuilder.Entity<TimeFrame>());
  }
}