using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Blazor.ComponentsLibrary;

public static  class ComponentLibraryServiceRegistration
{
    public static IServiceCollection AddComponentLibraryConfig(this IServiceCollection services)
    {
        //Dialog
        services.AddScoped<DialogService>();

        return services;
    }
}