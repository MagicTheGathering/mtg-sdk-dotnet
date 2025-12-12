# MTG SDK Avalonia Test Application

This is a cross-platform test application for the MTG SDK built with Avalonia UI, targeting .NET 8.0.

## Features

This application provides full feature parity with the WPF test application, including:

### Cards Tab
- **Search Cards**: Search for cards by name
- **Card List**: View search results with card names and Multiverse IDs
- **Find Selected**: Load detailed information for a selected card from the list
- **Card Details**: View card image and properties (Name, Multiverse ID, Artist, Layout, Power, Mana Cost, Rarity)
- **Type Queries**: Query and display card types, super types, sub types, and formats

### Set Tab
- **Search Sets**: Search for sets by name
- **Set List**: View search results with set names and codes
- **Find Selected**: Load detailed information for a selected set from the list
- **Set Details**: View set properties (Name, Block, Code, Gatherer Code, Old Code, Magic Cards Info Code, Release Date, Border, Expansion, Online Only, Type)
- **Generate Booster**: Generate a random booster pack for the selected set

### Additional Features
- **Loading Indicator**: Progress bar displays during API operations
- **Async/Await**: All operations are asynchronous and non-blocking
- **Shared Core Library**: Uses `MtgApiManager.Lib.TestApp.Core` for business logic, ensuring consistency with the WPF version

## Architecture

The application follows the MVVM pattern:

- **View**: `MainWindow.axaml` - Avalonia XAML UI
- **ViewModel**: `MainWindowViewModel.cs` - Presentation logic using CommunityToolkit.Mvvm
- **Model/Controller**: `MtgController` (from Core library) - Business logic and API calls

## Running the Application

### Prerequisites
- .NET 8.0 SDK or later
- Any platform supported by Avalonia (Windows, macOS, Linux)

### Build and Run
```bash
cd src/MtgApiManager.Lib.TestApp.Avalonia
dotnet run
```

### Build Release Version
```bash
dotnet build --configuration Release
```

## Implementation Notes

### Avalonia-Specific Adaptations

While maintaining feature parity with the WPF version, some Avalonia-specific implementations were necessary:

1. **Selection Binding**: Avalonia's `ListBox` selection model differs from WPF's `ListView`. We use intermediate properties (`SelectedCardFromList`, `SelectedSetFromList`) that update the ID/Code properties when selection changes.

2. **Control Mapping**:
   - WPF `ListView` → Avalonia `ListBox`
   - WPF `Label` → Avalonia `TextBlock` (for display-only text)
   - WPF `ProgressBar.IsIndeterminate` → Avalonia `ProgressBar.IsIndeterminate` (same behavior)

3. **Data Templates**: Avalonia's `DataTemplate` syntax is similar to WPF but uses slightly different binding conventions.

### Known Limitations

- None - all features from the WPF test app are fully implemented

## Dependencies

- **Avalonia** 11.3.8 - Cross-platform UI framework
- **CommunityToolkit.Mvvm** 8.4.0 - MVVM helpers (same as WPF version)
- **Microsoft.Extensions.DependencyInjection** 9.0.0 - Dependency injection
- **MtgApiManager.Lib** - The MTG SDK library
- **MtgApiManager.Lib.TestApp.Core** - Shared business logic

## Development

To extend or modify this application:

1. **Add UI Elements**: Edit `Views/MainWindow.axaml`
2. **Add Commands/Properties**: Edit `ViewModels/MainWindowViewModel.cs`
3. **Add Business Logic**: Edit `MtgController` in the Core library (shared with WPF version)

The ViewModel uses source generators from CommunityToolkit.Mvvm:
- `[ObservableProperty]` generates property change notifications
- `[RelayCommand]` generates command implementations
- `[NotifyCanExecuteChangedFor]` links properties to command execution state

## License

See the main repository LICENSE.md file.
