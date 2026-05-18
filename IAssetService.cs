using MyMauiApp.Models;

namespace MyMauiApp.Services;

public interface IAssetService
{
    List<Asset> GetAssets(); // Sync for the current ViewModel implementation
    Task<int> SaveAssetAsync(Asset asset);
    Task DeleteAssetAsync(Asset asset);
}