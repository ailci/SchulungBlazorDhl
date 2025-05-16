using Application.Contracts.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using UI.Blazor.Client;
using UI.Blazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

//Options-Pattern => DI
builder.Services.Configure<QotdAppSettings>(builder.Configuration.GetSection(nameof(QotdAppSettings)));
builder.Services.AddScoped<IQotdService, QotdApiService>();

//Named-Http-Client
//builder.Services.AddHttpClient("qotdapiservice", options =>
//{
//    options.BaseAddress = new Uri("https://localhost:7260");
//    options.DefaultRequestHeaders.Add("Accept", "application/json");
//});

var qotdAppSettings = builder.Configuration.GetSection(nameof(QotdAppSettings)).Get<QotdAppSettings>();

//Typed Client
builder.Services.AddHttpClient<IQotdService, QotdApiService>(client =>
{
    client.BaseAddress = new Uri(qotdAppSettings!.QotdServiceApiUri);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
