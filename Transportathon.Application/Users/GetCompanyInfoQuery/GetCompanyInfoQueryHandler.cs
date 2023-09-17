using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Users.GetCompanyVehicle;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Users.GetCompanyInfoQuery;

public class GetCompanyInfoQueryHandler : IQueryHandler<GetCompanyInfoQuery, List<CompanyBooking>>
{
    private readonly IBookingRepository _bookingRepository;

    public GetCompanyInfoQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Result<List<CompanyBooking>>> Handle(GetCompanyInfoQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.GetCompanyBookings(request.CompanyId, cancellationToken);

        return bookings.Select(b => new CompanyBooking
        {
            Id = b.Id,
            Reviews = b.Reviews.Select(r => new ReviewResponse
            {
                Id = r.User?.Id,
                UserName = r.User?.Name.Value,
                Comment = r.Comment.Value,
                Rating = r.Rating.Value,
                CreatedOnUtc = r.CreatedOnUtc
            }).ToList(),
            Vehicle = new VehicleResponse
            {
                Model = b.Vehicle?.Model.Value,
                Type = b.Vehicle?.Type.ToString(),
                Driver = b.Vehicle?.Driver?.Name.Value,
                Year = b.Vehicle?.Year.Value,
                NumberPlate = b.Vehicle?.NumberPlate.Value
            },
            Carriers = b.Vehicle?.Carriers.Select(c => new CarrierResponse()
            {
                Id = c.Id,
                Name = c.Name.Value,
                Profession = c.Profession.ToString(),
                Experience = c.Experience.Value
            }).ToList()
        }).ToList();
    }
}