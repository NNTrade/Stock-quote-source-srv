using service.dto;

namespace service.interactor;

public interface TimeFrameInteractor
{
  Task<IEnumerable<TimeFrameDto>> Get();
}