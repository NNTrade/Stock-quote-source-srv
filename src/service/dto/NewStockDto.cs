using database.Entity;

namespace service.dto;
public class NewStockDto
{
  public string Code { get; set;}

  public string Name { get; set;}

}

public static class NewStockDtoMapper{
  public static Stock ToEnity(this NewStockDto newStockDto){
    return new Stock() {Code = newStockDto.Code, Name = newStockDto.Name};
  }
}