using Application.Commands.CreateAzureCredentials;
using Application.Services.Interface;
using MediatR;

namespace Application.Services;

public class SecretGeneratorService:ISecretGeneratorService
{
    private readonly IMediator _mediator;

    public SecretGeneratorService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<dynamic> GenerateAzureAccessToken(string clientId)
    {
        var azureAccessTokenResponse = await _mediator.Send(new AzureAccessTokenCommand(clientId));
        return "";
    }
}