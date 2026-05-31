using System.Collections.ObjectModel;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; } = new();
    private readonly IAssetService _assetService;

    public event Action? OnStateChanged;

    public MainViewModel(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public async Task LoadAssetsAsync()
    {
        var data = await _assetService.GetAssetsAsync();
        Assets.Clear();
        foreach (var item in data)
            Assets.Add(item);

        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnStateChanged?.Invoke();
}