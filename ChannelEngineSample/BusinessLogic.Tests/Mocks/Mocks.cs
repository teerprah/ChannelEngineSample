using System.Net;
using System.Text;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Shared;
using Shared.ChannelEngineRestClient;

namespace BusinessLogic.Tests.Mocks;

public static class Mocks
{
    public static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string json) 
        where T : new()
    {
        var data = JsonConvert.DeserializeObject<T>(json);
        var response =  
        new RestResponse()
        {
            Content = json,
            StatusCode = httpStatusCode,
            IsSuccessStatusCode = true,
            ResponseStatus = ResponseStatus.Completed
        };

        var mockRestClient = new Mock<IRestClient>();
        mockRestClient
            .Setup(s => s.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .Callback<RestRequest, CancellationToken>((request, cancellationToken) =>
            {
                // response
                //     .Setup(s => s.Request)
                //     .Returns(request);
            })
            .ReturnsAsync(response);

        return mockRestClient.Object;
    }

    public static IChannelEngineRestClient MockChannelEngineRestClient(HttpStatusCode statusCode, string json )
    {
        return new MockChannelEngineRestClient(statusCode, json);
    }

}

