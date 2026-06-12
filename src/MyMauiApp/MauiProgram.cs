﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using MyMauiApp.Shared.Services; // Updated namespace for shared services
using MyMauiApp.Services;
// It's good practice to keep using statements sorted alphabetically for consistency.
namespace MyMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton<ICameraService, MauiCameraService>(); // Uses MyMauiApp.Services
		builder.Services.AddSingleton<ICartService, CartService>();         // Uses MyMauiApp.Shared.Services

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
