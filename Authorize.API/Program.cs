using Authorize.API.Extentions;
using Authorize.API.Middlewares;
using Authorize.Dal;
using Authorize.Infastructure;
using Authorize.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.ConfigureMediats();

builder.Services.ConfigureAppRepositories();

builder.Services.ConfigureAppServices();

builder.Services.ConfigureBackgroundServices();

builder.Services.ConfigureInfastructureServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
