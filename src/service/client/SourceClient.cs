using service.dto;

namespace service.client;
public interface SourceClient
{
  Task<List<QuoteDto>> Get(string tfCode, string stock, DateTime from, DateTime till);

}