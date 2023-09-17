using Transportathon.Application.Users.GetCompanyVehicle;

namespace Transportathon.Application.Users.GetCompanyInfoQuery;

public record CompanyBooking
{
    public Guid Id { get; set; }
    public List<ReviewResponse> Reviews { get; init; } = new();
    public VehicleResponse Vehicle { get; init; } = null!;

    public List<CarrierResponse>? Carriers { get; set; } = new();
}


public record ReviewResponse
{
    public string? UserName { get; init; }

    public int Rating { get; init; }

    public string? Comment { get; init; }

    public DateTime CreatedOnUtc { get; init; }
    public Guid? Id { get; set; }
}

public record VehicleResponse
{
    public string? Model { get; init; }

    public int? Year { get; init; }

    public string? Type { get; init; }
    public string? Driver { get; init; }
    public string? NumberPlate { get; init; }
}

public record CarrierResponse
{
    public string? Name { get;  init; }
    
    public int? Experience { get; init; }

    public string? Profession { get; init; }
    public Guid Id { get; set; }
}