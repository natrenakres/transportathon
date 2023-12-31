using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Application.Users.GetCompanyInfoQuery;
using Transportathon.Application.Users.GetCompanyVehicle;
using Transportathon.Application.Users.LogInUser;
using Transportathon.Application.Users.RegisterUser;

namespace Transportathon.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Name, request.Phone, request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LogIn(
        LogInUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(request.Email, request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        
        return Ok(result.Value);
    }

    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        return Ok();
    }
    
    [HttpGet("company/vehicle")]
    public async Task<IActionResult> GetCompanyVehicle(CancellationToken cancellationToken)
    {
        var query = new GetCompanyVehicleQuery();

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
    
    [HttpGet("company/{companyId:guid}/info")]
    public async Task<IActionResult> GetCompanyInfo(Guid companyId, CancellationToken cancellationToken)
    {
        var query = new GetCompanyInfoQuery(companyId);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}