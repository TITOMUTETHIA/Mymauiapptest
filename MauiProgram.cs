﻿﻿﻿﻿﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebView.Maui;
using MyMauiApp.Shared;
using MyMauiApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MyMauiApp.ViewModels;
using CommunityToolkit.Maui;

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
		builder.Services.AddSingleton<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
		builder.Services.AddAuthorizationCore(); // Required for AuthorizeRouteView
		builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
		
		// Register Data Services and ViewModels
		builder.Services.AddSingleton<IAssetService, SqliteAssetService>();
		builder.Services.AddTransient<MainViewModel>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
