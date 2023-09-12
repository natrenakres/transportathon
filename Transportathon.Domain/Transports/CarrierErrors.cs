using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Transports;

public static class CarrierErrors
{
    public static Error NotFound = new("Carrier.Found", "The Carrier for communication was not found");
}