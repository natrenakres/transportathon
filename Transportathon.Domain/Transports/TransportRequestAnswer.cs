using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;

namespace Transportathon.Domain.Transports;

public sealed class TransportRequestAnswer : Entity
{
    public TransportRequestAnswer(Guid id, Guid requestId, Guid companyId, Money price) : base(id)
    {
        RequestId = requestId;
        Price = price;
        CompanyId = companyId;
    }


    public Guid RequestId { get; private set; }

    public Guid CompanyId { get; private set; }

    public Money Price { get; private set; }

    public bool IsAcceptedFromMember { get; private set; }

    public static TransportRequestAnswer Create(TransportRequest request, Money price, Guid companyId)
    {
        var requestAnswer = new TransportRequestAnswer(Guid.NewGuid(), request.Id, companyId, price);
        
        request.Answers.Add(requestAnswer);

        return requestAnswer;
    }

    public void SetIsAccepted()
    {
        IsAcceptedFromMember = true;
    }
}