# ?? BeerRate MAUI App

A mobile beer rating application built with .NET MAUI for Android. Track and rate your favorite beers with persistent local storage.

## Features

- ? Rate beers on a 1-4 star scale
- ?? Track beer name, brewery, and style
- ?? Search and filter your beer ratings
- ?? View statistics (most popular styles, breweries, rating distribution)
- ?? Export your ratings to CSV
- ?? SQLite database for persistent storage
- ?? Native Android UI

## Technologies Used

- .NET 8
- .NET MAUI (Multi-platform App UI)
- Entity Framework Core
- SQLite
- MVVM Pattern with CommunityToolkit.Mvvm

## Prerequisites

- Visual Studio 2022 with .NET MAUI workload
- Android SDK (API 21+)
- .NET 8 SDK

## Installation

1. Clone this repository
2. Open `BeerRate-MAUI App.sln` in Visual Studio 2022
3. Connect your Android device or start an Android emulator
4. Press F5 to build and deploy

## Usage

1. **Add a Beer**: Enter beer name, brewery, select a style, and tap a rating (1-4 stars)
2. **Search**: Use the search bar to filter beers by name, brewery, or style
3. **View Stats**: Tap "View Stats" to see your rating statistics
4. **Export**: Tap "Export CSV" to share your beer ratings

## Data Persistence

All beer ratings are stored locally in a SQLite database on your device. Data persists across app restarts and phone reboots.

## License

This project is for personal use.
