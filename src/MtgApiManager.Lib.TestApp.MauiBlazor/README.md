# MTG SDK Blazor Hybrid Test Application

This is a cross-platform desktop test application for the MTG SDK built with Blazor and MudBlazor UI components, targeting .NET 8.0.

## Features

This application provides full feature parity with the Avalonia and WPF test applications, including:

### Cards Tab
- **Search Cards**: Search for cards by name
- **Card List**: View search results with card names and Multiverse IDs
- **Find Selected**: Load detailed information for a selected card from the list
- **Card Details**: View card image and properties (Name, Multiverse ID, Artist, Layout, Power, Mana Cost, Rarity)
- **Type Queries**: Query and display card types, super types, sub types, and formats

### Sets Tab
- **Search Sets**: Search for sets by name
- **Set List**: View search results with set names and codes
- **Find Selected**: Load detailed information for a selected set from the list
- **Set Details**: View set properties (Name, Block, Code, Gatherer Code, Old Code, Magic Cards Info Code, Release Date, Border, Expansion, Online Only, Type)
- **Generate Booster**: Generate a random booster pack for the selected set

### Additional Features
- **Loading Indicator**: Progress bar displays during API operations
- **Async/Await**: All operations are asynchronous and non-blocking
- **Shared Core Library**: Uses `MtgApiManager.Lib.TestApp.Core` for business logic, ensuring consistency with other test apps
- **Light/Dark Mode**: Toggle between light and dark themes using the button in the app bar
- **Modern UI**: Built with MudBlazor for a clean, Material Design-inspired interface

## Architecture

The application follows Blazor component architecture:

- **Pages**: `Home.razor` - Main page with Cards and Sets tabs
- **Layout**: `MainLayout.razor` - MudBlazor layout with theme toggle
- **Controller**: `MtgController` (from Core library) - Business logic and API calls
- **Services**: Dependency injection for MTG services and MudBlazor

## Running the Application

### Prerequisites
- .NET 8.0 SDK or later
- Any platform supported by Blazor (Windows, macOS, Linux)

### Build and Run
```bash
cd src/MtgApiManager.Lib.TestApp.MauiBlazor
dotnet run
```

The application will start a local web server (typically at https://localhost:5001 or http://localhost:5000) and automatically open in your default browser.

### Build Release Version
```bash
dotnet build --configuration Release
```

### Publish for Desktop
To create a standalone desktop application:
```bash
dotnet publish --configuration Release
```

## Implementation Notes

### Blazor-Specific Features

1. **Interactive Server Components**: The main page uses `@rendermode InteractiveServer` for real-time interactivity.

2. **MudBlazor Components**:
   - `MudTabs` - Tab navigation between Cards and Sets
   - `MudTextField` - Search input fields
   - `MudButton` - Action buttons
   - `MudList` - Displaying card and set lists
   - `MudPaper` - Container sections with elevation
   - `MudGrid` - Responsive layout
   - `MudProgressLinear` - Loading indicator
   - `MudThemeProvider` - Theme management with light/dark mode

3. **Theme Toggle**: The app bar includes a theme toggle button (sun/moon icon) that switches between light and dark modes.

4. **Responsive Design**: The layout adapts to different screen sizes using MudBlazor's grid system.

### Known Limitations

- None - all features from the Avalonia and WPF test apps are fully implemented

## Dependencies

- **MudBlazor** 7.23.0 - Material Design component library
- **MtgApiManager.Lib** - The MTG SDK library
- **MtgApiManager.Lib.TestApp.Core** - Shared business logic

## Development

To extend or modify this application:

1. **Add UI Elements**: Edit `Components/Pages/Home.razor`
2. **Modify Layout**: Edit `Components/Layout/MainLayout.razor`
3. **Add Business Logic**: Edit `MtgController` in the Core library (shared with other test apps)
4. **Customize Theme**: MudBlazor themes can be customized in the `MainLayout.razor` file

## Deployment

### Desktop Deployment Options

While this is a Blazor web application, it can be deployed as a desktop app using several approaches:

1. **Browser-based**: Run locally and access via browser (simplest approach)
2. **Electron.NET**: Package as an Electron desktop app
3. **WebView2**: Host in a native desktop application with WebView2
4. **Progressive Web App (PWA)**: Install as a PWA from the browser

### Web Deployment

The application can also be deployed to any web server that supports ASP.NET Core:

```bash
dotnet publish --configuration Release
# Deploy the contents of bin/Release/net8.0/publish/ to your web server
```

## License

See the main repository LICENSE.md file.
