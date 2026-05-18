using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp;

public class ModelImportService
{
    private readonly IAssetService _assetService;

    public ModelImportService(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public async Task<bool> Import3DModelAsync()
    {
        try
        {
            // 1. Define 3D Model file types
            var modelTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.item" } },
                { DevicePlatform.Android, new[] { "application/octet-stream" } },
                { DevicePlatform.WinUI, new[] { ".glb", ".gltf", ".obj", ".fbx", ".stl" } }
            });

            // 2. Pick the file
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select 3D Model",
                FileTypes = modelTypes
            });

            if (result == null) return false;

            // 3. Ensure a dedicated folder exists in AppDataDirectory
            string modelsDir = Path.Combine(FileSystem.AppDataDirectory, "Models");
            if (!Directory.Exists(modelsDir)) Directory.CreateDirectory(modelsDir);

            // 4. Copy file to local storage (prevent overwriting with unique names if needed)
            string destinationPath = Path.Combine(modelsDir, result.FileName);
            using (var sourceStream = await result.OpenReadAsync())
            using (var destStream = File.Create(destinationPath))
            {
                await sourceStream.CopyToAsync(destStream);
            }

            // 5. Save metadata to SQLite
            var asset = new Asset
            {
                Name = result.FileName,
                Category = "3D Model",
                LocalFilePath = destinationPath,
                Description = $"Imported model: {result.FileName}"
            };

            await _assetService.SaveAssetAsync(asset);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Import failed: {ex.Message}");
            return false;
        }
    }
}