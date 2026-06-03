using System.Collections.ObjectModel;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; } = new();
    private readonly IAssetService _assetService;
    private readonly IAiService _aiService;

    public event Action? OnStateChanged;

    public MainViewModel(IAssetService assetService, IAiService aiService)
    {
        _assetService = assetService;
        _aiService = aiService;
    }

    public async Task LoadAssetsAsync()
    {
        try
        {
            var data = await _assetService.GetAssetsAsync();
            Assets.Clear();
            foreach (var item in data)
                Assets.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load assets: {ex.Message}");
        }
        finally
        {
            NotifyStateChanged();
        }
    }

    public async Task AnalyzeAssetAsync(Asset asset)
    {
        if (asset == null || string.IsNullOrWhiteSpace(asset.Description)) return;

        try
        {
            asset.AiAnalysis = "Generating analysis...";
            NotifyStateChanged();

            asset.AiAnalysis = await _aiService.AnalyzeAssetAsync(asset.Description);
        }
        catch (Exception ex)
        {
            asset.AiAnalysis = "Analysis unavailable.";
            Console.WriteLine($"AI Analysis error: {ex.Message}");
        }
        finally
        {
            NotifyStateChanged();
        }
    }

    public void NotifyStateChanged() => OnStateChanged?.Invoke();
}