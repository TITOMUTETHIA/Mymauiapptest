﻿using MyMauiApp.Models;
using System.Collections.ObjectModel;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
	public ObservableCollection<Asset> Assets { get; set; } = new();

	public MainPage()
	{
		InitializeComponent();
		LoadAssets();
		AssetsCollection.ItemsSource = Assets;
	}

	private void LoadAssets()
	{
		Assets.Add(new Asset { 
			Name = "Low-Poly Forest Pack", 
			Category = "Environment", 
			Price = "$15.00", 
			ImageSource = "dotnet_bot.png" // Replace with actual blender renders
		});
		
		Assets.Add(new Asset { 
			Name = "Cyberpunk Character", 
			Category = "Characters", 
			Price = "$45.00", 
			ImageSource = "dotnet_bot.png" 
		});
	}
}
