namespace MyMauiApp.Shared.Services;

public interface ICameraService
{
    /// <summary>
    /// Captures a photo and returns the local path to the saved image, or null if cancelled.
    /// </summary>
    Task<string?> TakePhotoAsync();
}