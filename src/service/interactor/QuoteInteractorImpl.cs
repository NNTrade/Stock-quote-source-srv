using database;
using Microsoft.Extensions.Logging;
using service.client;
using service.dto;

namespace service.interactor;
public class QuoteInteractorImpl : QuoteInteractor
{
  private readonly SQSDbContext dbContext;
  private readonly ILogger<QuoteInteractorImpl> logger;
  private readonly SourceClientFactory sourceClientFactory;

  public QuoteInteractorImpl(SQSDbContext dbContext, ILogger<QuoteInteractorImpl> logger, SourceClientFactory sourceClientFactory)
  {
    this.dbContext = dbContext;
    this.logger = logger;
    this.sourceClientFactory = sourceClientFactory;
  }
  public Task<List<QuoteDto>> Get(string tfCode, string stock, DateTime from, DateTime till)
  {
    throw new NotImplementedException();
  }
}