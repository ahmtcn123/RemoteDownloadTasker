using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using Infrastructure.Extensions;
using Core.Extensions;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


//Initialize Api Extensions
builder.Services.AddApiServices();
builder.Services.AddApiJwtBearer(builder.Configuration);

//Initialize Infrastructure Extensions
builder.Services.AddInfrastructureApplicationDbContext(builder.Configuration);
builder.Services.AddInfrastructureRepositories();

//Initialize Core Extensions
builder.Services.AddCoreServices();
builder.Services.AddCoreMappings();
builder.Services.AddCoreValidators();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.AddApiMiddlewares();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
