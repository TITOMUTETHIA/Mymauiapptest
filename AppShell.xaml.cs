﻿namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Logout", "Are you sure you want to sign out?", "Yes", "No");
        if (result)
        {
            // Add your logout logic here (e.g., clear auth tokens, navigate to Login page)
        }
    }
}
