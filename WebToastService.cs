using MyMauiApp.Shared;

namespace MyMauiApp;

public class WebToastService : IToastService
{
    public void ShowToast(string message)
    {
        // For Blazor WebAssembly without JavaScript, we can only log to console.
        // In a real app, you might use a Blazor component to display a temporary message.
        Console.WriteLine($"Web Toast: {message}");
    }
}