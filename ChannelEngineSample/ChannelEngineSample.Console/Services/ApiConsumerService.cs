using BusinessLogic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Enums;

namespace ChannelEngineSample.Console.Services;

public class ApiConsumerService
{
    private readonly  Orders _orders;
    private readonly Products _products;
    private readonly ILogger<ApiConsumerService> _logger;

    public ApiConsumerService(Orders orders, Products products, ILogger<ApiConsumerService> logger)
    {
        _orders = orders;
        _products = products;
        _logger = logger;
    }
    public async Task Demonstrate()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        var getOrderResponse = await _orders.GetOrdersByStatus(statuses);
        if (!getOrderResponse.IsSuccessful)
        {
            System.Console.WriteLine(getOrderResponse.Content);
            _logger.LogError($"Failed to get orders: {getOrderResponse.Content}");
        }
        var orders = (JObject)JsonConvert.DeserializeObject(getOrderResponse.Content);
        var products = _orders.GetProductsFromOrders(orders);
        var topFiveBestSellingProducts = _orders.GetTopFiveBestSellingProducts(products);
        var patchProductStockResponse =  await _products.UpdateProductStock(topFiveBestSellingProducts.FirstOrDefault().MerchantProductNumber, 25);
        if (!patchProductStockResponse.IsSuccessful)
        {
            _logger.LogError($"Failed to patch products: {patchProductStockResponse.Content}");
            System.Console.WriteLine(patchProductStockResponse.Content);
        }
        else
        {
            _logger.LogInformation(
                $"Patch request completed! Response from server: {patchProductStockResponse.Content}");
            System.Console.WriteLine(JsonConvert.SerializeObject(topFiveBestSellingProducts));
        }

    }
}