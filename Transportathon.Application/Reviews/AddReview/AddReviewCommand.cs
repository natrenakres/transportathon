
using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Reviews.AddReview;

public record AddReviewCommand(Guid BookingId, int Rating, string Comment) : ICommand;