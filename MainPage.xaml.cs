﻿using MyMauiApp.ViewModels;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        // Passing data into the Blazor Root Component (Routes.razor)
        rootComponent.Parameters = new Dictionary<string, object?>
        {
            { "AppDataPath", FileSystem.AppDataDirectory }
        };
    }

    private async void OnAboutButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AboutPage");
    }

    private async void OnContactButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ContactPage");
    }
}