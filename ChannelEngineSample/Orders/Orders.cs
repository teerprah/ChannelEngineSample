using System.Text.Json.Nodes;
using RestSharp;
using Shared;
using Shared.Enums;

namespace Orders;

public class Orders
{
    private IChannelEngineRestClient _channelEngineRestClient;

    public Orders(IChannelEngineRestClient channelEngineRestClient)
    {
        _channelEngineRestClient = channelEngineRestClient;
    }
       
    public Task<JsonObject?> GetOrders(List<OrderStatus> statuses)
    {
        var request = _channelEngineRestClient.ChannelEngineRestRequest("/v2/orders");
        statuses.ForEach(status=> request.AddQueryParameter<>("statuses", status));
        return  _channelEngineRestClient.Client.GetAsync<JsonObject>(request);
    }
}