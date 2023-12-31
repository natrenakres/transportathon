using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Reviews;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Domain.Bookings;

public sealed class Booking : Entity
{
    public Booking(Guid id,
        Guid requestId, 
        Guid companyId, 
        Guid userId, 
        BookingStatus status, 
        DateTime beginDate, 
        Guid vehicleId, 
        Guid carrierIdForCommunication, 
        DateTime? estimatedEndDate): base(id)
    {
        TransportRequestId = requestId;
        CompanyId = companyId;
        UserId = userId;
        Status = status;
        BeginDate = beginDate;
        VehicleId = vehicleId;
        CarrierId = carrierIdForCommunication;
        EstimatedEndDate = estimatedEndDate;
    }

    private Booking() { }

    public Guid TransportRequestId { get; private set; }
    public TransportRequest? TransportRequest { get; private set; }

    public Guid CompanyId { get; private set; }
    public Company? Company { get; private set; }

    public Guid VehicleId { get; private set; }
    public Vehicle? Vehicle { get; private set; }

    public Guid CarrierId { get; private set; }
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public BookingStatus Status { get; private set; }
    
    public DateTime BeginDate { get; private set; }

    public DateTime? EstimatedEndDate { get; private set; }

    public List<Review> Reviews { get; private set; } = new();

    public static Booking Create(Guid requestId, Guid companyId, Guid userId, BookingStatus status, DateTime beginDate,
        Guid vehicleId, Guid carrierIdForCommunication, DateTime? estimatedEndDate = null)
    {
        var booking = new Booking( Guid.NewGuid(), requestId, companyId, userId, BookingStatus.Started, beginDate, vehicleId,
            carrierIdForCommunication, estimatedEndDate);


        return booking;
    }

    public Booking Complete()
    {
        Status = BookingStatus.Completed;
        return this;
    }

    public Booking Deliver()
    {
        Status = BookingStatus.Delivered;

        return this;
    }

    public Booking CancelByUser()
    {
        Status = BookingStatus.CancelledByUser;

        return this;
    }
    
    public Booking CancelByCompany()
    {
        Status = BookingStatus.CancelledByCompany;

        return this;
    }
    
    

}