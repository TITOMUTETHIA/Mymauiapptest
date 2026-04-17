﻿namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
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