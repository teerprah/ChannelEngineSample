using BusinessLogic;
using Shared;
using Shared.ChannelEngineRestClient;
using Shared.Enums;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<ChannelEngineApiConfig>();
builder.Services.Configure<ChannelEngineApiConfig>(builder.Configuration.GetSection("ChannelEngineApiConfig"));
builder.Services.AddSingleton<IChannelEngineRestClient, ChannelEngineRestClient>();
builder.Services.AddSingleton<Orders>();
builder.Services.AddMvcCore();
var app = builder.Build();

app.MapGet("/", () => "Hello World");
app.MapControllers();

app.Run();