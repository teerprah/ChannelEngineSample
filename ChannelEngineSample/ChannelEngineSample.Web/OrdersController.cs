using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Enums;

namespace ChannelEngineSample.Web;

//set as the default route
[Route("")]
[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly Orders _orders;
    private readonly Products _products;
    private ILogger<OrdersController> _logger;

    public OrdersController(Orders orders, Products products, ILogger<OrdersController> logger)
    {
        _orders = orders;
        _products = products;
        _logger = logger;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        var getOrderResponse = await _orders.GetOrdersByStatus(statuses);
        if (!getOrderResponse.IsSuccessful) return BadRequest(getOrderResponse.Content);
        var orders = (JObject)JsonConvert.DeserializeObject(getOrderResponse.Content);
        var products = _orders.GetProductsFromOrders(orders);
        var topFiveBestSellingProducts = _orders.GetTopFiveBestSellingProducts(products);
        var patchProductStockResponse =  await _products.UpdateProductStock(topFiveBestSellingProducts.FirstOrDefault().MerchantProductNumber, 25);
        if (!patchProductStockResponse.IsSuccessful)
        {
            _logger.LogError($"Failed to patch products: {patchProductStockResponse.Content}");
            return BadRequest(patchProductStockResponse.Content);
        }
        else
        {
            _logger.LogInformation($"Patch request completed! Response from server: {patchProductStockResponse.Content}");
        }
        return View(topFiveBestSellingProducts);

    }
    
}