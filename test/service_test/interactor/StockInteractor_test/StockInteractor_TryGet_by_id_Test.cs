using database;
using database.Entity;
using Microsoft.Extensions.Logging;
using service.dto;
using service.interactor;
using service_test.tool;
using Xunit.Abstractions;

namespace service_test.interactor.StockInteractor_test;

public class StockInteractor_TryGet_by_code_Test : BaseTest
{

  SQSDbContext test_dbContext;
  ILogger<StockInteractorImpl> test_logger;
  private readonly StockInteractor stockInteractor;

  public StockInteractor_TryGet_by_code_Test(ITestOutputHelper output) : base(nameof(StockInteractor_TryGet_by_code_Test), output)
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
  public async void WHEN_get_THEN_get_all()
  {
    // Array
    List<Stock> stocks = new List<Stock>();
    using (var arrayContext = CreateContext())
    {
      var arrayInteractor = new StockInteractorImpl(arrayContext, test_logger);
      stocks.Add(arrayInteractor.Add(new NewStockDto() { Code = "RUB/USD", Name = "RUB for Dollar" }).Result);
      stocks.Add(arrayInteractor.Add(new NewStockDto() { Code = "APPL", Name = "RUB for Dollar" }).Result);
    }
    var expected_stock = stocks[0];

    // Act
    var asserted_tuple = await stockInteractor.TryGet(expected_stock.Code);
    var asserted_stock = asserted_tuple.Value;

    // Assert
    Assert.True(asserted_tuple.IsFound);
    Assert.Equal(expected_stock.Id, asserted_stock.Id);
    Assert.Equal(expected_stock.Code, asserted_stock.Code);
    Assert.Equal(expected_stock.Name, asserted_stock.Name);

  }

  [Fact]
  public async Task WHEN_get_no_stock_THEN_empty_listAsync()
  {
    // Array

    // Act

    // Assert
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await stockInteractor.Get("Stock"));
  }
}