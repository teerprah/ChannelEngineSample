using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Shared.Enums;

namespace Shared.Models;

public class JsonPatchOperationModel
{
    [JsonProperty("op")]
    public string PatchOpertaion { get; set; }
    
    [JsonProperty("value")]
    public object Value { get; set; }
    
    
    [JsonProperty("path")]
    public string Path { get; set; }
    

    

}