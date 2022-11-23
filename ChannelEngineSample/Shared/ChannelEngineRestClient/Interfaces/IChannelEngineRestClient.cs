using System.Text.Json.Nodes;
using RestSharp;
using Shared.Enums;

namespace Shared;

public interface IChannelEngineRestClient
{
    Task<JsonObject?> GetOrders(List<OrderStatus> statuses);
}