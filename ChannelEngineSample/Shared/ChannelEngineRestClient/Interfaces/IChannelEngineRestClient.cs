using System.Text.Json.Nodes;
using RestSharp;
using Shared.Enums;

namespace Shared;

public interface IChannelEngineRestClient
{
    IRestClient Client { get; }
    RestRequest ChannelEngineRestRequest(string endpoint);
}