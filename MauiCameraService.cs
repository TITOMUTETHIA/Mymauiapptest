using MyMauiApp.Shared;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;

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

                // Load the captured photo into a Graphics image object
                using Stream sourceStream = await photo.OpenReadAsync();
                using IImage image = PlatformImage.FromStream(sourceStream);

                if (image == null) return null;

                // Resize the image to a maximum of 1024px on its longest side.
                // We set disposeOriginal to false because the 'using' block above now handles 'image'.
                using IImage resizedImage = image.Downsize(1024, disposeOriginal: false);

                // Save the resized image to the cache directory
                string localPath = Path.Combine(FileSystem.CacheDirectory, "resized_" + photo.FileName);
                using FileStream localFileStream = File.OpenWrite(localPath);
                
                await resizedImage.SaveAsync(localFileStream, ImageFormat.Jpeg);
                return localPath;
            }
            
            Console.WriteLine("Camera capture is not supported on this device.");
            return null;
        }
        catch (PermissionException pEx)
        {
            Console.WriteLine($"Camera permission denied: {pEx.Message}");
            throw; // Propagate to UI
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing photo with MAUI MediaPicker: {ex.Message}");
            throw; // Propagate to UI
        }
    }
}