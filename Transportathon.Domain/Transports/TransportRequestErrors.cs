using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Transports;

public static class TransportRequestErrors
{
    public static Error NotFound = new("TransportRequest.Found", "The request for answer was not found");
}