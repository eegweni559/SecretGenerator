using System.Text.Json;
using Domain.Constants;
using Domain.Models;
using MediatR;


namespace Application.Queries.GetClientDetails;

public record GetClientDetailsQuery(string displayName) ;

    public class GetClientDetailsQueryHandler 
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetClientDetailsQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<dynamic> Handle(GetClientDetailsQuery getClientDetailsRequest, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(SecretGeneratorConstants.MicrosoftLoginClient);
            var request = new HttpRequestMessage(HttpMethod.Get, "");
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("displayName",getClientDetailsRequest.displayName),

            };
            request.Content = new FormUrlEncodedContent(body);
            var response = await client.SendAsync(request,cancellationToken);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync() ?? throw new Exception("Unable to complete request");
        }
        
    }

    
