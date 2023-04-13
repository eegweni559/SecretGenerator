namespace Domain.Models;

public class AzureAccessTokenSettings
{
    public string scope { get; set; }
    public string clientSecret { get; set; }
    public string grantType { get; set; }
}