using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;