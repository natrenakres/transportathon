using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Application.Transports.AddTransportRequest;
using Transportathon.Application.Transports.AnswerTransportRequest;
using Transportathon.Application.Transports.GetAllTransportRequest;
using Transportathon.Application.Transports.GetTransportRequest;
using Transportathon.Application.Transports.GetTransportRequestAnswers;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Api.Controllers.Transports;

[ApiController]
[Route("api/transport/request")]
[Authorize]
public class TransportRequestController : ControllerBase
{
    private readonly ISender _sender;

    public TransportRequestController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransportRequests(CancellationToken cancellationToken)
    {
        var query = new GetAllTransportRequestQuery();
        var results = await _sender.Send(query, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results.Error);
        }

        return Ok(results.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTransportRequest(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTransportRequestQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        
        return Ok(result.Value);
    }


    [HttpPost]
    public async Task<IActionResult> AddTransportRequest(TransportRequest request, CancellationToken cancellationToken)
    {
        var type = request.Type switch
        {
            2 => TransportRequestType.OfficeTransport,
            3 => TransportRequestType.BigVolumeAndHeavy,
            _ => TransportRequestType.HomeToHome
        };

        var command = new AddTransportRequestCommand(
            request.BeginDate,
            type, 
            new Description(request.Description),
            new Address(request.Address.Country, request.Address.State, request.Address.ZipCode, request.Address.City,
                request.Address.Street));

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetTransportRequest), new { id = result.Value }, result.Value);
    }


    [HttpPost("{requestId:guid}/answer")]
    public async Task<IActionResult> AnswerTransportRequest(Guid requestId, AnswerTransportRequest answer)
    {
        var command = new AnswerTransportRequestCommand(requestId,
            new Money(answer.Amount, Currency.FromCode(answer.Currency)));

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpGet("{requestId:guid}/answers")]
    public async Task<IActionResult> GetTransportRequestAnswers(Guid requestId, CancellationToken cancellationToken)
    {
        var query = new GetTransportRequestAnswersQuery(requestId);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}