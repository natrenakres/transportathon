using FluentValidation;

namespace Transportathon.Application.Transports.AddTransportRequest;

public class AddTransportRequestCommandValidator : AbstractValidator<AddTransportRequestCommand>
{
    public AddTransportRequestCommandValidator()
    {
        RuleFor(c => c.BeginDate).GreaterThan(DateTime.UtcNow);
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Type).NotEmpty();
        
    }
}