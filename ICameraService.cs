namespace MyMauiApp.Shared;

public interface ICameraService
{
    // Returns the image path or base64 string
    Task<string?> TakePhotoAsync();
}