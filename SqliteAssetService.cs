using SQLite;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp;

public class SqliteAssetService : IAssetService
{
    private SQLiteConnection? _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_data.db3");

    private void Init()
    {
        if (_db is not null) return;

        _db = new SQLiteConnection(_dbPath);
        _db.CreateTable<Asset>();
    }

    public List<Asset> GetAssets()
    {
        Init();
        return _db!.Table<Asset>().ToList();
    }

    public async Task<int> SaveAssetAsync(Asset asset)
    {
        Init();
        if (asset.Id != 0)
            return _db!.Update(asset);
        else
            return _db!.Insert(asset);
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        Init();
        _db!.Delete(asset);
        await Task.CompletedTask;
    }
}