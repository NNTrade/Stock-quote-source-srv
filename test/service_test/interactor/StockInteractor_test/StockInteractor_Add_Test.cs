using database;
using Microsoft.Extensions.Logging;
using service.dto;
using service.interactor;
using service_test.tool;
using Xunit.Abstractions;

namespace service_test.interactor.StockInteractor_test;

public class StockInteractor_Add_Test : BaseTest
{
  SQSDbContext test_dbContext;
  ILogger<StockInteractorImpl> test_logger;
  private readonly StockInteractor stockInteractor;
  public StockInteractor_Add_Test(ITestOutputHelper output) : base(nameof(StockInteractor_Add_Test), output)
  {
    test_logger = output.BuildLoggerFor<StockInteractorImpl>();
    test_dbContext = this.CreateContext();
    stockInteractor = new StockInteractorImpl(test_dbContext, test_logger);
  }
  public virtual void Dispose()
  {
    test_dbContext.Dispose();
    base.Dispose();
  }

  [Fact]
  public async void WHEN_add_stock_THEN_create_entityAsync()
  {
    // Array
    var newStock = new NewStockDto() { Code = "RUB/USD", Name = "RUB for Dollar" };

    // Act
    var assertedEntity = await stockInteractor.Add(newStock);

    // Assert
    Assert.Equal(newStock.Code, assertedEntity.Code);
    Assert.Equal(newStock.Name, assertedEntity.Name);
  }

  [Fact]
  public async Task WHEN_add_dub_code_THEN_exceptionAsync()
  {
    // Array
    var newStock = new NewStockDto() { Code = "RUB/USD", Name = "RUB for Dollar" };
    using (var arrayContext = CreateContext())
    {
      var arrayInteractor = new StockInteractorImpl(arrayContext, test_logger);
      await arrayInteractor.Add(newStock);
    }

    // Act

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(async () => await stockInteractor.Add(newStock));
  }

  [Fact]
  public async Task WHEN_no_code_THEN_exceptionAsync()
  {
    // Array
    var newStock = new NewStockDto() { Name = "RUB for Dollar" };
    // Act

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(async () => await stockInteractor.Add(newStock));
  }
}
