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
                FileResult? photo = await Microsoft.Maui.Media.MediaPicker.Default.CapturePhotoAsync();
                
                if (photo == null) return null; // User cancelled

                // Ensure the filename is safe and construct path
                string localPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localPath);
                
                await sourceStream.CopyToAsync(localFileStream);
                return localPath;
            }
            
            Console.WriteLine("Camera capture is not supported on this device.");
            return null;
        }
        catch (PermissionException pEx)
        {
            Console.WriteLine($"Camera permission denied: {pEx.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing photo with MAUI MediaPicker: {ex.Message}");
            return null;
        }
    }
}