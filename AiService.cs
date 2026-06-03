using Microsoft.Extensions.AI;

namespace MyMauiApp.Services;

public class AiService : IAiService
{
    private readonly IChatClient _chatClient;

    public AiService(IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> AnalyzeAssetAsync(string assetDescription)
    {
        try
        {
            var response = await _chatClient.GetResponseAsync(
                $"Analyze this asset and provide a brief summary: {assetDescription}");
            
            return response.Text ?? "No analysis available.";
        }
        catch (Exception ex)
        {
            return $"Error during analysis: {ex.Message}";
        }
    }
}