using System;
using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.TestApp.Core;
using Xunit;

namespace MtgApiManager.Lib.TestApp.Core.Test;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddMtgApiManager_RegistersMtgServiceProvider()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var mtgServiceProvider = serviceProvider.GetService<IMtgServiceProvider>();
        Assert.NotNull(mtgServiceProvider);
        Assert.IsAssignableFrom<IMtgServiceProvider>(mtgServiceProvider);
    }

    [Fact]
    public void AddMtgApiManager_RegistersMtgController()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var controller = serviceProvider.GetService<MtgController>();
        Assert.NotNull(controller);
        Assert.IsType<MtgController>(controller);
    }

    [Fact]
    public void AddMtgApiManager_MtgServiceProviderIsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var instance1 = serviceProvider.GetService<IMtgServiceProvider>();
        var instance2 = serviceProvider.GetService<IMtgServiceProvider>();

        // Assert
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void AddMtgApiManager_MtgControllerIsTransient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var instance1 = serviceProvider.GetService<MtgController>();
        var instance2 = serviceProvider.GetService<MtgController>();

        // Assert
        Assert.NotSame(instance1, instance2);
    }

    [Fact]
    public void AddMtgApiManager_ReturnsServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddMtgApiManager();

        // Assert
        Assert.Same(services, result);
    }

    [Fact]
    public void AddMtgApiManager_MtgControllerCanAccessCardService()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var controller = serviceProvider.GetRequiredService<MtgController>();
        var mtgServiceProvider = serviceProvider.GetRequiredService<IMtgServiceProvider>();
        var cardService = mtgServiceProvider.GetCardService();

        // Assert
        Assert.NotNull(cardService);
    }

    [Fact]
    public void AddMtgApiManager_MtgControllerCanAccessSetService()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMtgApiManager();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var controller = serviceProvider.GetRequiredService<MtgController>();
        var mtgServiceProvider = serviceProvider.GetRequiredService<IMtgServiceProvider>();
        var setService = mtgServiceProvider.GetSetService();

        // Assert
        Assert.NotNull(setService);
    }
}
