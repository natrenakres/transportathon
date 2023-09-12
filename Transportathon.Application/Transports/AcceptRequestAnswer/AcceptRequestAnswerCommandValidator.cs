using FluentValidation;

namespace Transportathon.Application.Transports.AcceptRequestAnswer;

public class AcceptRequestAnswerCommandValidator : AbstractValidator<AcceptRequestAnswerCommand>
{
    public AcceptRequestAnswerCommandValidator()
    {
        RuleFor(c => c.TransportRequestId).NotEmpty();
        RuleFor(c => c.TransportRequestAnswerId).NotEmpty();
        
    }
}