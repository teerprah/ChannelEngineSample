using System.Text.Json.Nodes;
using RestSharp;
using Shared;
using Shared.Enums;

namespace BusinessLogic;

public class Products
{
    private readonly IChannelEngineRestClient _channelEngineRestClient;

    public Products(IChannelEngineRestClient channelEngineRestClient)
    {
        _channelEngineRestClient = channelEngineRestClient;
    }
       
    public Task<JsonArray?> UpsertProducts(JsonArray products)
    {
        var request = _channelEngineRestClient.ChannelEngineRestRequest("/v2/products");
        request.AddJsonBody(products);
        return  _channelEngineRestClient.Client.PostAsync<JsonArray>(request);
    }
}