using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReserveBookingCommandHandler(ITransportRequestRepository transportRequestRepository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository,
        IBookingRepository bookingRepository)
    {
        _transportRequestRepository = transportRequestRepository;
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var transportRequest = await _transportRequestRepository.GetWithAnswersAsync(request.TransportRequestId, cancellationToken);
        
        if (transportRequest is null)
        {
            return Result.Failure<Guid>(TransportRequestErrors.NotFound);
        }

        transportRequest.SetBooked();
        var answer = transportRequest.Answers.FirstOrDefault(c => c.IsAcceptedFromMember);

        if (answer is null)
        {
            return Result.Failure<Guid>(TransportRequestAnswerErrors.NotFound);
        }

        var company = await _companyRepository.GetByIdAsync(answer.CompanyId, cancellationToken);
        var vehicle = company?.Vehicles.FirstOrDefault(x => x.Id == request.VehicleId);
        var carrier = vehicle?.Carriers.FirstOrDefault(x => x.CanCommunicateWithMember);
        
        if (carrier is null)
        {
            return Result.Failure<Guid>(CarrierErrors.NotFound);
        }
        
        var booking =  Booking.Create(request.TransportRequestId, answer.CompanyId, transportRequest.UserId, BookingStatus.Started,
            transportRequest.BeginDate,
            request.VehicleId, carrier.Id);
        
        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return booking.Id;
    }
}