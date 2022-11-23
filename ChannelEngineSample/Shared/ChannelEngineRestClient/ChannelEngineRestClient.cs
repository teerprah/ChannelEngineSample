using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using RestSharp;
using Shared.Enums;

namespace Shared.ChannelEngineRestClient;

public class ChannelEngineRestClient: IChannelEngineRestClient, IDisposable
{
    public RestClient Client { get; }
    private readonly string _apiKey;
    
    public ChannelEngineRestClient(IOptions<ChannelEngineApiConfig> apiConfig)
    {
        var options = new RestClientOptions(apiConfig.Value.BaseUrl);
        Client = new RestClient(options);
        _apiKey = apiConfig.Value.ApiKey;
    }
    
    

    public RestRequest ChannelEngineRestRequest(string endpoint)
    {
        var request = new RestRequest(endpoint);
        request.AddQueryParameter("apikey", _apiKey);
        return request;
    }
    
    
    public void Dispose() {
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }


}