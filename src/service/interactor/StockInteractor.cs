using database.Entity;
using service.dto;

namespace service.interactor;

public interface StockInteractor{
  Task<Stock> Add(NewStockDto newStockDto);

  Task<IEnumerable<Stock>> Get();

  Task<Stock> Get(int Id);
  Task<Stock> Get(string Code);
  
  Task<TryDto<Stock>> TryGet(int Id);
  Task<TryDto<Stock>> TryGet(string Code);
}