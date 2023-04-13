using Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI.SecretGeneratorHttpClient;

public static class SecretGeneratorHttpClient
{
    public static IServiceCollection AddSecretGeneratorHttpClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient(SecretGeneratorConstants.MicrosoftLoginClient)
            .ConfigureHttpClient((_, client) =>
            {
                client.BaseAddress = new Uri(configuration[""]);
            });
        return services;
    }
}