using Microsoft.Extensions.Logging;

namespace service;
public static class EventIdFactory
{

  public static EventId AddEntityFailEvent => new EventId(1, "Fail add entity to db");
}