using MyMauiApp.Shared;

namespace MyMauiApp;

public class WebToastService : IToastService
{
    public event Action<string>? OnShow;

    public void ShowToast(string message) => OnShow?.Invoke(message);
}