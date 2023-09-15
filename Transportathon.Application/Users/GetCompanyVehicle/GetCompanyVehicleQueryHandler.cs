using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Users.GetCompanyVehicle;


public class GetCompanyVehicleQueryHandler : IQueryHandler<GetCompanyVehicleQuery, CompanyResponse>
{
    private readonly IUserRepository _userRepository;

    public GetCompanyVehicleQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<CompanyResponse>> Handle(GetCompanyVehicleQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetCompanyAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<CompanyResponse>(UserErrors.NotFound);
        }

        var company = user.Company;
        
        if (company is null)
        {
            return Result.Failure<CompanyResponse>(UserErrors.NotFound);
        }

        return new CompanyResponse()
        {
            Name = company.Name.Value,
            Vehicles = company.Vehicles.Select(v => new VehicleResponse(v.Id, v.NumberPlate.Value)).ToList()
        };
    }
}