using HrLeaveManagement.Api;
using HrLeaveManagement.Api.Middleware;
using HrLeaveManagement.Application;
using HrLeaveManagement.Identity;
using HrLeaveManagement.Infrastructure;
using HrLeaveManagement.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("all", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddApiServices();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseSerilogRequestLogging();

app.UseCors("all");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();