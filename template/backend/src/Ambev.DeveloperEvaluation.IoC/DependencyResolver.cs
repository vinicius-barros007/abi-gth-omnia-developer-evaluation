using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC;

public static class DependencyResolver
{
    public static void RegisterDependencies(this WebApplicationBuilder builder)
    {
        new ApplicationModuleInitializer().Initialize(builder);
        new InfrastructureModuleInitializer().Initialize(builder);
        new WebApiModuleInitializer().Initialize(builder);

        builder.Services.AddScoped<IDiscountService, DiscountService>();    
    }
}