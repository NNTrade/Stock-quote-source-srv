using System.ComponentModel.DataAnnotations;
using database;
using database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using service.dto;

namespace service.interactor;
public class StockInteractorImpl : StockInteractor
{
  private readonly SQSDbContext dbContext;
  private readonly ILogger<StockInteractorImpl> logger;

  public StockInteractorImpl(SQSDbContext dbContext, ILogger<StockInteractorImpl> logger)
  {
    this.dbContext = dbContext;
    this.logger = logger;
  }

  public async Task<Stock> Add(NewStockDto newStockDto)
  {
    logger.LogInformation("Start add new stock");

    var newEntity = newStockDto.ToEnity();

    logger.LogDebug("Validating new stock entity");
    var context = new ValidationContext(newEntity);
    var results = new List<ValidationResult>();
    if (!Validator.TryValidateObject(newEntity, context, results, true))
    {
      logger.LogInformation("New Stock not valid");
      var errorList = results.SelectMany(error => error.MemberNames.Select(field => new ArgumentException(error.ErrorMessage, field))).ToArray();
      throw new ArgumentException("New stock not valid", new AggregateException("Found Errors", errorList));
    }

    logger.LogDebug("Add stock to db");
    try
    {
      await dbContext.Stocks.AddAsync(newEntity);
      await dbContext.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      if (ex.InnerException != null && ex.InnerException is PostgresException)
      {
        PostgresException pgEx = (PostgresException)ex.InnerException;
        if (pgEx.Code == "23505")
        {
          logger.LogInformation("New Stock is duplicate");
          throw new ArgumentException($"Stock with code {newStockDto.Code} already exist", nameof(newStockDto.Code));
        }
      }
      logger.LogCritical(EventIdFactory.AddEntityFailEvent, ex, "Failed to add new Stock to db:\n{0}", JsonConvert.SerializeObject(newStockDto));
      throw new ApplicationException($"Cann't save stock");
    }
    catch (Exception ex)
    {
      logger.LogCritical(EventIdFactory.AddEntityFailEvent, ex, "Failed to add new Stock to db:\n{0}", JsonConvert.SerializeObject(newStockDto));
      throw new ApplicationException($"Cann't save stock");
    }
    return newEntity;
  }

  public async Task<IEnumerable<Stock>> Get()
  {
    return await dbContext.Stocks.ToListAsync();
  }

  public async Task<Stock> Get(int id)
  {
    var ret = await TryGet(id);
    if (!ret.IsFound) throw new ArgumentOutOfRangeException(nameof(id), id, $"There is no stock with id {id}");
    return ret.Value!;
  }

  public async Task<Stock> Get(string code)
  {
    var ret = await TryGet(code);
    if (!ret.IsFound) throw new ArgumentOutOfRangeException(nameof(code), code, $"There is no stock with code {code}");
    return ret.Value!;
  }

  public async Task<TryDto<Stock>> TryGet(int id)
  {
    var ret = await dbContext.Stocks.SingleOrDefaultAsync(s=>s.Id == id);
    return new TryDto<Stock>(ret);
  }

  public async Task<TryDto<Stock>> TryGet(string code)
  {
    var ret = await dbContext.Stocks.SingleOrDefaultAsync(s=>s.Code == code);
    return new TryDto<Stock>(ret);
  }
}