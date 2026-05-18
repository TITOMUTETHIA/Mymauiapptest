using MyMauiApp.Shared;

namespace MyMauiApp;

public class MauiCameraService : ICameraService
{
    public async Task<string?> TakePhotoAsync()
    {
        try
        {
            if (Microsoft.Maui.Media.MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await Microsoft.Maui.Media.MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    // Save to local path or return stream
                    var localPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using var sourceStream = await photo.OpenReadAsync();
                    using var localFileStream = File.OpenWrite(localPath);
                    await sourceStream.CopyToAsync(localFileStream);
                    return localPath;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing photo with MAUI MediaPicker: {ex.Message}");
            // Optionally, log the exception or show a user-friendly message
            return null;
        }
    }
}