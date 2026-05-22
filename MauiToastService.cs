using MyMauiApp.Shared;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;

namespace MyMauiApp;

public class MauiToastService : IToastService
{
    public void ShowToast(string message)
    {
        // IToast is a simple way to show platform-specific toasts in MAUI.
        // For more advanced toasts (duration, position, actions), consider CommunityToolkit.Maui.Alerts.Toast
        var toast = Toast.Make(message, ToastDuration.Short);
        MainThread.BeginInvokeOnMainThread(async () => await toast.Show());
    }
}