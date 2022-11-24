using System.Net;
using System.Text.Json.Nodes;
using BusinessLogic.Tests.Mocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;
using Shared.ChannelEngineRestClient;
using NUnit.Framework;
using Shared.Enums;

namespace BusinessLogic.Tests;
[TestFixture]
public class OrdersTests
{
    private JObject DummyOrdersJson;
    private BusinessLogic.Orders _orders;
    private IChannelEngineRestClient _mockcChannelEngineRestClient;
    [SetUp]
    public void Setup()
    {
        var jsonData = LoadJson("TestOrders.json");
        _mockcChannelEngineRestClient = new MockChannelEngineRestClient(HttpStatusCode.OK, jsonData );
        _orders = new BusinessLogic.Orders(_mockcChannelEngineRestClient);
        DummyOrdersJson = JsonConvert.DeserializeObject<JObject>(jsonData);
    }

    [Test]
    public async Task TestTopFiveBestSellingProductsOutput()
    {
        var statuses = new List<OrderStatus>() { OrderStatus.InProgress };
        var getOrderResponse = await _orders.GetOrdersByStatus(statuses);
        Assert.True(getOrderResponse.IsSuccessful);
        var orders = (JObject)JsonConvert.DeserializeObject(getOrderResponse.Content);
        var products = _orders.GetProductsFromOrders(orders);
        var topFiveBestSellingProducts = _orders.GetTopFiveBestSellingProducts(products).ToList();
        Assert.Greater(topFiveBestSellingProducts.Count(), 0);
        //From the JSON data, 001201-S should be the top selling product with qty 30
        Assert.That(30, Is.EqualTo(topFiveBestSellingProducts.FirstOrDefault().Quantity));
        Assert.That("Test Object with size S", Is.EqualTo(topFiveBestSellingProducts.FirstOrDefault().ProductName));
        //From the JSOn data, 001201-M should be the second with 18
        Assert.That(18, Is.EqualTo(topFiveBestSellingProducts[1].Quantity));
        Assert.That("Test Object with size M", Is.EqualTo(topFiveBestSellingProducts[1].ProductName));
        Assert.Pass();
    }
    
    private string LoadJson(string fileName)
    {
        using var r = new StreamReader(fileName);
        var json = r.ReadToEnd();
        return json;
    }

}