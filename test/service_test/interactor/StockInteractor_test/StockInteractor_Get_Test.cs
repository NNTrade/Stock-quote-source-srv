using database;
using database.Entity;
using Microsoft.Extensions.Logging;
using service.dto;
using service.interactor;
using service_test.tool;
using Xunit.Abstractions;

namespace service_test.interactor.StockInteractor_test;

public class StockInteractor_Get_Test : BaseTest
{

  SQSDbContext test_dbContext;
  ILogger<StockInteractorImpl> test_logger;
  private readonly StockInteractor stockInteractor;

  public StockInteractor_Get_Test(ITestOutputHelper output) : base(nameof(StockInteractor_Get_Test), output)
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

    // Act
    var asserted_list = await stockInteractor.Get();

    // Assert
    Assert.Equal(stocks.Count, asserted_list.Count());
    foreach (var stock in stocks)
    {
      var asserted_stock = asserted_list.Single(s => s.Id == stock.Id);
      Assert.Equal(stock.Code, asserted_stock.Code);
      Assert.Equal(stock.Name, asserted_stock.Name);
    }
  }

  [Fact]
  public async Task WHEN_get_no_stock_THEN_empty_listAsync()
  {
    // Array

    // Act
    var asserted_list = await stockInteractor.Get();

    // Assert
    Assert.Empty(asserted_list);
  }
}