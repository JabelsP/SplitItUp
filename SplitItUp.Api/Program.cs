using Serilog;
using SplitItUp.Api;
using SplitItUp.Api.ErrorHandling;
using SplitItUp.Application;
using SplitItUp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DbConnectionString") ??
                       throw new ArgumentException("Missing connection string");

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAppDbContext(connectionString);
builder.Services.AddApplicationServices();
builder.Services.AddProblemDetails();

builder.Services.AddAuthenticationAndAuthorization();


builder.Services.AddOpenTelemetryWithExporter();

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog(logger);

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


public partial class Program
{
}