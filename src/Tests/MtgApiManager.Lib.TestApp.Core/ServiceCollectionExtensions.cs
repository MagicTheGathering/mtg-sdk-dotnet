using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.TestApp.Core;

/// <summary>
/// Extension methods for registering MtgApiManager services with dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all MtgApiManager services including the MtgController and service provider.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddMtgApiManager(this IServiceCollection services)
    {
        // Register the MtgServiceProvider as singleton
        services.AddSingleton<IMtgServiceProvider, MtgServiceProvider>();
        
        // Register the MtgController as transient (can also be scoped or singleton depending on needs)
        services.AddTransient<MtgController>();
        
        return services;
    }
}
