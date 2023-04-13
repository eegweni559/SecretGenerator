namespace Application.Services.Interface;

public interface ISecretGeneratorService
{
    Task<dynamic> GenerateAzureAccessToken(string clientId);
}