using System.Net;
using System.Text.Json.Nodes;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using Shared;
using Shared.Enums;
using Shared.Models;

namespace BusinessLogic;

public class Products
{
    private readonly IChannelEngineRestClient _channelEngineRestClient;

    public Products(IChannelEngineRestClient channelEngineRestClient)
    {
        _channelEngineRestClient = channelEngineRestClient;
    }

    public async Task<RestResponse> UpdateProductStock(string merchantProductNumber, int quantity)
    {
        var patchOperationModelList = new List<JsonPatchOperationModel>()
        {
            new()
            {
                PatchOpertaion = JsonPatchOpertaion.Replace.Name,
                Value = quantity,
                Path = "stock"
            }
        };

        var request = _channelEngineRestClient.ChannelEngineRestRequest($"/v2/products/{merchantProductNumber}");
        request.AddStringBody(JsonConvert.SerializeObject(patchOperationModelList), "application/json-patch+json");
        return await _channelEngineRestClient.Client.ExecuteAsync(request, Method.Patch);
    }
}