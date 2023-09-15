using MediatR;
using Microsoft.AspNetCore.Http;
using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Abstractions.Behaviors;

public class BaseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest :  IBaseMessage
{
    private readonly HttpContext? _httpContext;

    public BaseBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_httpContext is null) return await next();
        
        if (request is not BaseRequest baseRequest) return await next();
        
        var userId = _httpContext.User.Claims?.FirstOrDefault(x => x.Type == "sub")?.Value;
        if (userId is not null)
        {
            baseRequest.UserId = Guid.Parse(userId);
        }
        
        baseRequest.IsOwner = _httpContext.User.Claims?.FirstOrDefault(x => x.Type == "role")?.Value == "owner";
        
        return await next() ;
    }
}