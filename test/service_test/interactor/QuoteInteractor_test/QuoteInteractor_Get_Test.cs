using System.Net;
using database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using service.client;
using service.interactor;
using service_test.tool;
using Xunit.Abstractions;
public class QuoteInteractor_Get_Test : BaseTest
{
  SQSDbContext test_dbContext;
  ILogger<QuoteInteractorImpl> test_logger;
  private readonly QuoteInteractor quoteInteractor;
  private readonly SourceClientFactory sourceClientFactory = Substitute.For<SourceClientFactory>();
  public QuoteInteractor_Get_Test(ITestOutputHelper output) : base(nameof(QuoteInteractor_Get_Test), output)
  {
    test_logger = output.BuildLoggerFor<QuoteInteractorImpl>();
    test_dbContext = this.CreateContext();
    quoteInteractor = new QuoteInteractorImpl(test_dbContext, test_logger, sourceClientFactory);
  }
  public virtual void Dispose()
  {
    test_dbContext.Dispose();
    base.Dispose();
  }

  [Fact]
  public async Task WHEN_get_new_data_THEN_loadAsync()
  {
    // Array
    var expected_tf = "1D";
    var expected_code = "APPL";
    var expected_from = new DateTime(2000,1,1);
    var expected_till = new DateTime(2001,1,1);

    // Act
    var asserted_quote = await quoteInteractor.Get(expected_tf, expected_code, expected_from, expected_till);
    
    // Assert
  }


  [Fact]
  public async Task WHEN_get_till_less_from_THEN_errorAsync()
  {
    // Array
    var expected_tf = "1D";
    var expected_code = "APPL";
    var expected_from = new DateTime(2000,1,2);
    var expected_till = new DateTime(2000,1,1);
    // Act
    
    // Assert
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async ()=>await quoteInteractor.Get(expected_tf, expected_code, expected_from, expected_till));
  }

  [Fact]
  public void WHEN_get_loaded_data_THEN_no_load()
  {
    // Array

    // Act

    // Assert
  }

  [Fact]
  public void WHEN_get_part_new_data_THEN_part_load()
  {
    // Array

    // Act

    // Assert
  }

  [Fact]
  public void WHEN_get_loaded_less_priority_data_THEN_reload_load()
  {
    // Array

    // Act

    // Assert
  }
}