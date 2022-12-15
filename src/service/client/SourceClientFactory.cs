namespace service.client;
public interface SourceClientFactory{
  SourceClient Get(string Name);
  SourceClient Get(int id);
}