using System.Collections.ObjectModel;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; } = new();

    public MainViewModel(IAssetService assetService)
    {
        _ = InitializeAsync(assetService);
    }

    private async Task InitializeAsync(IAssetService assetService)
    {
        var data = await assetService.GetAssetsAsync();
        foreach (var item in data)
            Assets.Add(item);
    }
}