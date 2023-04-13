using Application.DI.SecretGeneratorHttpClient;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSecretGeneratorHttpClient(configuration);
        services.Configure<AzureAccessTokenSettings>(configuration.GetSection("AzureAccessTokenSettings"));
        return services;
    }
}