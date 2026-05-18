using Microsoft.JSInterop;
using MyMauiApp.Shared;

namespace MyMauiApp;

public class WebCameraService : ICameraService
{
    private readonly IJSRuntime _js;
    public WebCameraService(IJSRuntime js) => _js = js;

    public async Task<string?> TakePhotoAsync()
    {
        try
        {
            return await _js.InvokeAsync<string>("captureWebcamImage");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing image: {ex.Message}");
            throw; // Propagate to UI
        }
    }
}