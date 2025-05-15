using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using UI.Blazor.Client.Pages;
using UI.Blazor.Components;
using UI.Blazor.Components.Account;
using UI.Blazor.Components.Pages;
using UI.Blazor.ComponentsLibrary;
using UI.Blazor.Configuration;
using UI.Blazor.Data;
using UI.Blazor.Hub;
using UI.Blazor.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddBlazorConfig()
       .AddAuthenticationConfig()
       .AddSignalRConfig();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddComponentLibraryConfig();

var app = builder.Build();

// Configure the HTTP request pipeline. ##################################################
//app.Use(async (context, next) =>
//{
//    var userAgent = context.Request.Headers["User-Agent"][0].ToLower();
//    await context.Response.WriteAsync($"Erste middleware\n {userAgent}");
//    await next();
//    await context.Response.WriteAsync("Erste Back middleware\n");
//});
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Zweite middleware\n");
//    await next();
//    await context.Response.WriteAsync("Zweite Back middleware\n");
//});
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("End middleware\n");
//});

//app.UseBrowserAllowed(Browser.Chrome, Browser.Edge);

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(UI.Blazor.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapHub<AuthorLocalHub>("/authorLocalHub"); // Lokale Version mit BlazorServer als Hub

app.Run();
