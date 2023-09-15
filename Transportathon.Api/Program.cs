using System.IdentityModel.Tokens.Jwt;
using Transportathon.Api.Extensions;
using Transportathon.Application;
using Transportathon.Infrastructure;
using Transportathon.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
    app.SeedData();
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandler();


app.MapControllers();

app.Run();