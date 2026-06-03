namespace MyMauiApp.Services;

public interface IAiService
{
    Task<string> AnalyzeAssetAsync(string assetDescription);
}