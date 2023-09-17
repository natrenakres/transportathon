using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Transports;

public static class TransportRequestErrors
{
    public static Error NotFound = new("TransportRequest.Found", "The request was not found");
}

public static class TransportRequestAnswerErrors
{
    public static Error NotFound = new("TransportRequestAnswer.Found", "The answer of request was not found");
}

public static class CompanyErrors
{
    public static Error NotFound = new("Company.Found", "The company was not found");
}