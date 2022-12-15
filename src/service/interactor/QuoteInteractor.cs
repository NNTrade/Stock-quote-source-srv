using service.dto;

namespace service.interactor;
public interface QuoteInteractor {
  Task<List<QuoteDto>> Get(string tfCode, string stock, DateTime from, DateTime till);
}