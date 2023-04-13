using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Constants;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.CreateAzureCredentials;

public record AzureAccessTokenCommand(string ClientId): IRequest<CreateAccessToken>;

public class AzureAccessTokenCommandHandler : IRequestHandler<AzureAccessTokenCommand, CreateAccessToken>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AzureAccessTokenSettings _azureAccessTokenSettings;

    public AzureAccessTokenCommandHandler(IHttpClientFactory httpClientFactory,IOptions<AzureAccessTokenSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _azureAccessTokenSettings = options.Value;
    }
    public async Task<CreateAccessToken> Handle(AzureAccessTokenCommand accessTokenRequest, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(SecretGeneratorConstants.MicrosoftLoginClient);
        var request = new HttpRequestMessage(HttpMethod.Post, "");
        var body = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("scope",_azureAccessTokenSettings.scope),
            new KeyValuePair<string, string>("client_id",accessTokenRequest.ClientId),
            new KeyValuePair<string, string>("client_secret",_azureAccessTokenSettings.clientSecret),
            new KeyValuePair<string, string>("grant_type",_azureAccessTokenSettings.grantType)

        };
        request.Content = new FormUrlEncodedContent(body);
        var response = await client.SendAsync(request,cancellationToken);

        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<CreateAccessToken>((await response.Content.ReadAsStringAsync())) ?? throw new Exception("Unable to complete request");
    }
}
