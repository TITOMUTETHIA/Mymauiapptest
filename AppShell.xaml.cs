﻿﻿﻿namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register routes for navigation. Fixes the bug where native buttons 
		// on the landing page couldn't find the target pages.
		Routing.RegisterRoute("AboutPage", typeof(MainPage)); // Replace with actual page types if different
		Routing.RegisterRoute("ContactPage", typeof(MainPage));
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
