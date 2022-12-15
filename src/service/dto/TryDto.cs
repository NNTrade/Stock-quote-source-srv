namespace service.dto;

public class TryDto<TDto>
{
  public readonly bool IsFound;
  public readonly TDto? Value;
  public TryDto(bool status, TDto? dto)
  {
    IsFound = status;
    Value = dto;
  }

  public TryDto(TDto? dto)
  {
    IsFound = dto != null;
    Value = dto;
  }
}