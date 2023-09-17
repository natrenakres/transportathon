using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Users.GetCompanyInfoQuery;

public record GetCompanyInfoQuery(Guid CompanyId) : BaseRequest, IQuery<List<CompanyBooking>>;