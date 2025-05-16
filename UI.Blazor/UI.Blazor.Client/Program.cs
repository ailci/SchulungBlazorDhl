using Application.Contracts.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using UI.Blazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped<IQotdService, QotdApiService>();

//Named-Http-Client
//builder.Services.AddHttpClient("qotdapiservice", options =>
//{
//    options.BaseAddress = new Uri("https://localhost:7260");
//    options.DefaultRequestHeaders.Add("Accept", "application/json");
//});

//Typed Client
builder.Services.AddHttpClient<IQotdService, QotdApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7260");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
