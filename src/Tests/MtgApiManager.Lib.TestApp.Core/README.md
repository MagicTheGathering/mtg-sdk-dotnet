# MtgApiManager.Lib.TestApp.Core

This is a reusable core library that contains shared business logic for MTG test applications.

## Purpose

This library extracts common functionality from UI applications (WPF, Avalonia, etc.) to enable code reuse across different UI frameworks.

## Architecture

- **MtgController**: Main controller class that wraps MTG SDK services with simplified async methods
- **ServiceCollectionExtensions**: Dependency injection setup for easy service registration

## Usage

The library is already integrated with:
- WPF Test Application (`MtgApiManager.Lib.TestApp`)
- Avalonia Test Application (`MtgApiManager.Lib.TestApp.Avalonia`)

Both applications use `MtgController` for all business logic, keeping UI code focused on views and bindings.

## Features

- Async operations for all MTG API calls
- Loading state management via `IsSearching` property and `IsSearchingChanged` event
- Simplified error handling (returns empty collections on failure)
- Dependency injection support
