using Transportathon.Domain.Shared;

namespace Transportathon.Application.Users.GetCompanyVehicle;

public record CompanyResponse()
{
    public string Name { get; init; } = null!;
    public List<VehicleResponse> Vehicles { get; init; } = new();
}