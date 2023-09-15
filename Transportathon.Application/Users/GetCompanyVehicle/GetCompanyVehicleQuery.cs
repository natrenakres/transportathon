using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Users.GetCompanyVehicle;

public record GetCompanyVehicleQuery() : BaseRequest, IQuery<CompanyResponse>;