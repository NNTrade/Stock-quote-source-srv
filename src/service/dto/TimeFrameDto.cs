using database.Entity;

namespace service.dto;

public class TimeFrameDto
{
  public string Code { get; set; }
  public string Name { get; set; }
  public int Seconds { get; set; }
}

public static class TimeFrameDtoMapper
{
  public static TimeFrameDto ToDto(this TimeFrame timeFrame)
  {
    return new TimeFrameDto() { Code = timeFrame.Code, Seconds = timeFrame.Seconds, Name = timeFrame.Name };
  }
}