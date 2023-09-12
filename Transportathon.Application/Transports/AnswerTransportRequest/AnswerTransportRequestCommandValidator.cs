using FluentValidation;

namespace Transportathon.Application.Transports.AnswerTransportRequest;

public class AnswerTransportRequestCommandValidator : AbstractValidator<AnswerTransportRequestCommand>
{
    public AnswerTransportRequestCommandValidator()
    {
        RuleFor(c => c.Price).NotEmpty();
        RuleFor(c => c.TransportRequestId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        
    }
}