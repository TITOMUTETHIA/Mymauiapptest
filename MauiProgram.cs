﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using MyMauiApp.Shared;
using MyMauiApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MyMauiApp.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.AI;

namespace MyMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton<ICameraService, MauiCameraService>();
		builder.Services.AddSingleton<IToastService, MauiToastService>();
		builder.Services.AddCascadingAuthenticationState(); // Fixes auth visibility on the landing page
		builder.Services.AddSingleton<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
		builder.Services.AddAuthorizationCore(); // Required for AuthorizeRouteView
		builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

		// AI Integration: Using Microsoft.Extensions.AI abstractions
		// Improved registration to ensure singleton behavior
		builder.Services.AddChatClient(sp => new SampleChatClient()); 
		
		builder.Services.AddSingleton<IAiService, AiService>();
		
		// Register Data Services and ViewModels
		builder.Services.AddSingleton<IAssetService, SqliteAssetService>();
		builder.Services.AddSingleton<MainViewModel>();

#if WINDOWS
        // Windows-specific configuration, like window sizing or specialized desktop services
        builder.Logging.AddFilter("Microsoft.UI.Xaml", LogLevel.Warning);
#elif ANDROID
        // Android-specific configuration, such as setting up specific Intent filters or permissions
        builder.Logging.AddFilter("Android.Graphics", LogLevel.Debug);
#endif

		// Register Pages for DI
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
