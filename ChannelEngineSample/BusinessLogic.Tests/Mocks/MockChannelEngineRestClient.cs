using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using Shared;
using Shared.ChannelEngineRestClient;

namespace BusinessLogic.Tests.Mocks;

public class MockChannelEngineRestClient : IChannelEngineRestClient, IDisposable
{
    public IRestClient Client { get; }

    public MockChannelEngineRestClient(HttpStatusCode httpStatusCode, string json)
    {
        Client = Mocks.MockRestClient<JObject>(httpStatusCode, json);
    }
    
    

    public RestRequest ChannelEngineRestRequest(string endpoint)
    {
        var request = new RestRequest(endpoint);
        return request;
    }


    public void Dispose() {
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }


}