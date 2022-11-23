using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace ChannelEngineSample.Web;

[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly Orders _orders;

    public OrdersController(Orders orders)
    {
        _orders = orders;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        return  Ok(await _orders.GetOrders(statuses));
    }
}