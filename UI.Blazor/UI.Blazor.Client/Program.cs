using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

//Named-Http-Client
builder.Services.AddHttpClient("qotdapiservice", options =>
{
    options.BaseAddress = new Uri("https://localhost:7260");
    options.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
