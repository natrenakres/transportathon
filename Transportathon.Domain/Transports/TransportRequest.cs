using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Users;

namespace Transportathon.Domain.Transports;

public sealed class TransportRequest : Entity
{
    public TransportRequest(Guid id, Description description, DateTime beginDate,
        TransportRequestType type, Address address, DateTime? estimatedEndDate) : base(id)
    {
        Description = description;
        BeginDate = beginDate;
        Type = type;
        EstimatedEndDate = estimatedEndDate;
        Address = address;
    }

    

    public DateTime BeginDate { get; private set; }

    public DateTime? EstimatedEndDate { get; private set; }

    public TransportRequestType Type { get; private set; }
    public TransportRequestStatus Status { get; private set; }
    

    public bool IsCompleted { get; private set; }
    
    public Address Address { get; private set; }

    public Money? Price { get; private set; }

    public Description Description { get; private set; }

    public Guid UserId { get; private set; }

    public List<TransportRequestAnswer> Answers { get; private set; } = new();
    
    

    public static TransportRequest Create(Description description, DateTime beginDate,
        TransportRequestType type, Address address, DateTime? estimatedEndDate = null)
    {
        var transportRequest = new TransportRequest(Guid.NewGuid(), description,
            beginDate, type, address, estimatedEndDate);

        return transportRequest;
    }

    public TransportRequest Accept(Guid answerId, Money price)
    {
        ChangeStatus(TransportRequestStatus.Accepted);
        var answer = Answers.FirstOrDefault(x => x.Id == answerId);
        answer?.SetIsAccepted();
        SetPrice(price);

        return this;
    }

    private void SetPrice(Money price)
    {
        Price = price;
    }

    private void ChangeStatus(TransportRequestStatus status)
    {
        Status = status;
    }


    public void SetBooked()
    {
        ChangeStatus(TransportRequestStatus.Booked);
    }
}