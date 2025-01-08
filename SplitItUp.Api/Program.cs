using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using SplitItUp.Api.ErrorHandling;
using SplitItUp.Application;
using SplitItUp.Infrastructure;
using ExportProcessorType = OpenTelemetry.ExportProcessorType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddApplicationServices();
builder.Services.AddProblemDetails();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("split-it-up"))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation();

        tracing.AddOtlpExporter(_ =>
        {
            _.Endpoint = new Uri("http://172.17.0.1:4317");
            _.ExportProcessorType = ExportProcessorType.Batch;
            _.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
        });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation();

        metrics
            .AddOtlpExporter(_ =>
            {
                _.Endpoint = new Uri("http://172.17.0.1:4317");
                _.ExportProcessorType = ExportProcessorType.Batch;
                _.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
            });
    });

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
app.MapControllers();

app.Run();