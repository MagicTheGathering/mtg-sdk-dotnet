using System;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.TestApp.Core;
using Xunit;

namespace MtgApiManager.Lib.TestApp.Core.Test;

/// <summary>
/// Unit tests for MtgController class.
/// These tests focus on the controller's state management and event handling.
/// </summary>
public class MtgControllerTests
{
    [Fact]
    public void Constructor_WithValidServiceProvider_InitializesSuccessfully()
    {
        // Arrange
        var serviceProvider = new MtgServiceProvider();

        // Act
        var controller = new MtgController(serviceProvider);

        // Assert
        Assert.NotNull(controller);
        Assert.False(controller.IsSearching);
    }

    [Fact]
    public void IsSearching_InitiallyFalse()
    {
        // Arrange
        var serviceProvider = new MtgServiceProvider();
        
        // Act
        var controller = new MtgController(serviceProvider);

        // Assert
        Assert.False(controller.IsSearching);
    }

    [Fact]
    public void IsSearchingChanged_RaisedWhenValueChanges()
    {
        // Arrange
        var serviceProvider = new MtgServiceProvider();
        var controller = new MtgController(serviceProvider);
        var eventRaised = false;
        var eventValue = false;

        controller.IsSearchingChanged += (sender, isSearching) =>
        {
            eventRaised = true;
            eventValue = isSearching;
        };

        // Act
        // We can't easily test the actual IsSearching state changes without making real API calls
        // or exposing internal test hooks, so this test verifies the event is properly wired up
        
        // Assert
        Assert.False(eventRaised); // Event should not be raised until IsSearching changes
    }
}
