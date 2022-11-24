using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Shared;
using Shared.Enums;
using Shared.Models;

namespace BusinessLogic;

public class Orders
{
    private readonly IChannelEngineRestClient _channelEngineRestClient;

    public Orders(IChannelEngineRestClient channelEngineRestClient)
    {
        _channelEngineRestClient = channelEngineRestClient;
    }

    public async Task<RestResponse> GetOrdersByStatus(List<OrderStatus> statuses)
    {
        var request = _channelEngineRestClient.ChannelEngineRestRequest("/v2/orders");
        statuses.ForEach(status => request.AddParameter("statuses", status.Name));
        return await _channelEngineRestClient.Client.GetAsync(request);
    }

    public IEnumerable<JToken>? GetProductsFromOrders(JObject orders)
    {
        var content = orders["Content"]?.AsJEnumerable();
        var products = content?.SelectMany<JToken, JToken>(c => c?["Lines"].AsJEnumerable());
        return products;
    }

    public IEnumerable<ProductModel> GetTopFiveBestSellingProducts(IEnumerable<JToken> products)
    {
        var groupedProducts = products
            .GroupBy(p => p["Description"].ToString(),
                p => p,
                (key, group) =>
                {
                    var firstItem = group.FirstOrDefault();
                    return new ProductModel()
                    {
                        ProductName = key,
                        Quantity = group.Count(),
                        GTIN = firstItem["Gtin"].ToString(),
                        MerchantProductNumber = firstItem["MerchantProductNo"].ToString()
                    };
                }
                    
            )
            .OrderByDescending(p => p.Quantity)
            .Take(5);
        return groupedProducts;
    }
}