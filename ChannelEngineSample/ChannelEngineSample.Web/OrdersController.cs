using System.Text.Json.Nodes;
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

    public OrdersController(Orders orders, Products products)
    {
        _orders = orders;
        _products = products;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        var getOrderResponse = await _orders.GetOrdersByStatus(statuses);
        if (!getOrderResponse.IsSuccessful) return BadRequest(getOrderResponse.Content);
        var orders = (JObject)JsonConvert.DeserializeObject(getOrderResponse.Content);
        var products = _orders.GetProductsFromOrder(orders);
        var topFiveBestSellingProducts = _orders.GetTopFiveBestSellingProducts(products);
        var patchProductStockResponse =  await _products.UpdateProductStock(topFiveBestSellingProducts.FirstOrDefault().MerchantProductNumber, 25);
        if (!patchProductStockResponse.IsSuccessful)
        {
            return BadRequest(patchProductStockResponse.Content);
        }
        return View(topFiveBestSellingProducts);

    }
    
}