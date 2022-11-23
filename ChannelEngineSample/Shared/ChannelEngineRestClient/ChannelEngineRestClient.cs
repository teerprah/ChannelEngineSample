using System.Text.Json.Nodes;
using RestSharp;
using Shared.Enums;

namespace Shared.ChannelEngineRestClient;

public class ChannelEngineRestClient: IChannelEngineRestClient, IDisposable
{
    private readonly RestClient _client;
    private readonly string _apiKey;
    
    public ChannelEngineRestClient(string baseUrl, string apiKey)
    {
        var options = new RestClientOptions(baseUrl);
        _client = new RestClient(options);
        _apiKey = apiKey;
    }

    public Task<JsonObject?> GetOrders(List<OrderStatus> statuses)
    {
        var request = ChannelEngineRestRequest("/v2/orders", _apiKey);
        statuses.ForEach(status=> request.AddQueryParameter<>("statuses", status));
        return  _client.GetAsync<JsonObject>(request);
    }


    private RestRequest ChannelEngineRestRequest(string endpoint, string apiKey)
    {
        var request = new RestRequest(endpoint);
        request.AddQueryParameter("apikey", apiKey);
        return request;
    }
    
    
    public void Dispose() {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }


}