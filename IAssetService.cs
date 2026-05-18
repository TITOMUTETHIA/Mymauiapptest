using MyMauiApp.Models;

namespace MyMauiApp.Services;

public interface IAssetService
{
    Task<List<Asset>> GetAssetsAsync(); 
    Task<int> SaveAssetAsync(Asset asset);
    Task DeleteAssetAsync(Asset asset);
}