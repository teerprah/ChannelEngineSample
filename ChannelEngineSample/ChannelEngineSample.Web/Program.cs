using BusinessLogic;
using Shared;
using Shared.ChannelEngineRestClient;
using Shared.Enums;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<ChannelEngineApiConfig>();
builder.Services.Configure<ChannelEngineApiConfig>(builder.Configuration.GetSection("ChannelEngineApiConfig"));
builder.Services.AddSingleton<IChannelEngineRestClient, ChannelEngineRestClient>();
builder.Services.AddSingleton<Orders>();
builder.Services.AddSingleton<Products>();
builder.Services.AddMvcCore().AddRazorViewEngine();
var app = builder.Build();
app.MapControllers();

app.Run();