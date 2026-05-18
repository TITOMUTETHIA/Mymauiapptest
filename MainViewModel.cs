using System.Collections.ObjectModel;
using MyMauiApp.Models;
using MyMauiApp.Services; // Assuming you'd put the service here

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; }

    public MainViewModel(IAssetService assetService)
    {
        Assets = new ObservableCollection<Asset>(assetService.GetAssets());
    }
}