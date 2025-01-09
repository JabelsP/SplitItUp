using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ExportProcessorType = OpenTelemetry.ExportProcessorType;

namespace SplitItUp.Api;

public static class ServiceCollectionExtensions
{
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/SplitItUp"; // Keycloak URL
                options.MetadataAddress = "http://localhost:8080/realms/SplitItUp/.well-known/openid-configuration";
                options.RequireHttpsMetadata = false;
        
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:8080/realms/SplitItUp",
                    ValidateAudience = true,
                    ValidAudience = "split-it-up",
                };
            });

        services.AddAuthorization();
    }

    public static void AddOpenTelemetryWithExporter(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
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
    }

}