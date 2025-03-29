using Authorize.API.Extentions;
using Authorize.API.Middlewares;
using Authorize.Dal;
using Authorize.Infastructure;
using Authorize.Application;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.ConfigureValidators();

builder.Services.ConfigureAppRepositories();
builder.Services.ConfigureAppServices();
builder.Services.ConfigureInfastructureServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
