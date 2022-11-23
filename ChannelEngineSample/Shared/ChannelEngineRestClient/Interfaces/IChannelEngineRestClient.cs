using System.Text.Json.Nodes;
using RestSharp;
using Shared.Enums;

namespace Shared;

public interface IChannelEngineRestClient
{
    RestClient Client { get; }
    RestRequest ChannelEngineRestRequest(string endpoint);
}