using BusinessLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Enums;

namespace ChannelEngineSample.Console;

public class ApiConsumerService
{
    private readonly  Orders _orders;
    private readonly Products _products;

    public ApiConsumerService(Orders orders, Products products)
    {
        _orders = orders;
        _products = products;
    }
    public async Task Demonstrate()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        var getOrderResponse = await _orders.GetOrdersByStatus(statuses);
        if (!getOrderResponse.IsSuccessful) System.Console.WriteLine(getOrderResponse.Content);
        var orders = (JObject)JsonConvert.DeserializeObject(getOrderResponse.Content);
        var products = _orders.GetProductsFromOrders(orders);
        var topFiveBestSellingProducts = _orders.GetTopFiveBestSellingProducts(products);
        var patchProductStockResponse =  await _products.UpdateProductStock(topFiveBestSellingProducts.FirstOrDefault().MerchantProductNumber, 25);
        if (!patchProductStockResponse.IsSuccessful)
        {
            System.Console.WriteLine(patchProductStockResponse.Content);
        }
        System.Console.WriteLine(JsonConvert.SerializeObject(topFiveBestSellingProducts));
        
    }
}