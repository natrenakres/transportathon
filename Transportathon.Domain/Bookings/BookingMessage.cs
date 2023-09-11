using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Bookings;

public sealed class BookingMessage : Entity
{
    public BookingMessage(Guid id) : base(id)
    {
        
    }

    public MessageText Text { get; private set; }

    public MessageFrom From { get; private set; }
}