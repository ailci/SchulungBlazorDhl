using Application.Contracts.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.Blazor.Components.Account;
using UI.Blazor.Data;
using UI.Blazor.Services;

namespace UI.Blazor.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddBlazorConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents() //Componenten
            .AddInteractiveServerComponents()  //Blazor Server
            .AddInteractiveWebAssemblyComponents() // Blazor WebAssembly
            .AddAuthenticationStateSerialization();

        //DI
        builder.Services.AddScoped<IQotdService, QotdService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();

        return builder;
    }

    public static WebApplicationBuilder AddSignalRConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();

        return builder;
    }
    
    public static WebApplicationBuilder AddAuthenticationConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return builder;
    }
}