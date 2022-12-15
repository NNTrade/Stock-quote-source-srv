using System.Net;
using database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using service.dto;
using service.interactor;
using service_test.tool;
using Xunit.Abstractions;
public class TimeFrameInteractor_Get_Test : BaseTest
{
  SQSDbContext test_dbContext;
  ILogger<TimeFrameInteractorImpl> test_logger;
  private readonly TimeFrameInteractor timeframeInteractor;
  public TimeFrameInteractor_Get_Test(ITestOutputHelper output) : base(nameof(TimeFrameInteractor_Get_Test), output)
  {
    test_logger = output.BuildLoggerFor<TimeFrameInteractorImpl>();
    test_dbContext = this.CreateContext();
    timeframeInteractor = new TimeFrameInteractorImpl(test_dbContext, test_logger);
  }

  [Fact]
  public async void WHEN_get_THEN_get_dto()
  {
    // Array
    var expected_tf_list = new List<TimeFrameDto>(){
        new TimeFrameDto(){Code = "1D", Name = "Day", Seconds = 24*60*60},
        new TimeFrameDto(){Code = "1H", Name = "Hour", Seconds = 60*60}
      };

    // Act
    var asserted_tf_list = await timeframeInteractor.Get();

    // Assert
    Assert.Equal(2, asserted_tf_list.Count());
    foreach (var expected_tf in expected_tf_list)
    {
      var asserted_tf = asserted_tf_list.Single(tf => tf.Code == expected_tf.Code);
      Assert.Equal(expected_tf.Name, asserted_tf.Name);
      Assert.Equal(expected_tf.Seconds, asserted_tf.Seconds);
    }

  }
}