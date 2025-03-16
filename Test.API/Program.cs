using Hangfire;
using Test.API.Extentions;
using Test.API.Middlewares;
using Test.Application;
using Test.Dal;
using Test.Infastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddRabbitMq(builder.Configuration);

builder.Services.AddDalRepositories();
builder.Services.ConfigureInfastructureServices(builder.Configuration);
builder.Services.ConfigureAppServices();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
