using BusinessLogic;
using Serilog;
using Shared;
using Shared.ChannelEngineRestClient;


var builder = WebApplication.CreateBuilder(args);

//Add logging using Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.Configure<ChannelEngineApiConfig>(builder.Configuration.GetSection("ChannelEngineApiConfig"));
builder.Services.AddSingleton<IChannelEngineRestClient, ChannelEngineRestClient>();
builder.Services.AddSingleton<Orders>();
builder.Services.AddSingleton<Products>();
builder.Services.AddMvcCore().AddRazorViewEngine();
var app = builder.Build();
app.MapControllers();

app.Run();