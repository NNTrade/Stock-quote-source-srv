using database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using service.dto;

namespace service.interactor;
public class TimeFrameInteractorImpl : TimeFrameInteractor
{
  private readonly SQSDbContext dbContext;
  private readonly ILogger<TimeFrameInteractorImpl> logger;

  public TimeFrameInteractorImpl(SQSDbContext dbContext, ILogger<TimeFrameInteractorImpl> logger)
  {
    this.dbContext = dbContext;
    this.logger = logger;
  }

  public async Task<IEnumerable<TimeFrameDto>> Get()
  {
    return await dbContext.TimeFrames.Select(tf=>tf.ToDto()).ToArrayAsync();
  }
}