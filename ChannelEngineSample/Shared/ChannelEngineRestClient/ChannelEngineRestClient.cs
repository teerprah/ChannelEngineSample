using System.Text.Json.Nodes;
using RestSharp;
using Shared.Enums;

namespace Shared.ChannelEngineRestClient;

public class ChannelEngineRestClient: IChannelEngineRestClient, IDisposable
{
    public RestClient Client { get; }
    private readonly string _apiKey;
    
    public ChannelEngineRestClient(string baseUrl, string apiKey)
    {
        var options = new RestClientOptions(baseUrl);
        Client = new RestClient(options);
        _apiKey = apiKey;
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