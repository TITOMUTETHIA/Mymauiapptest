using SQLite;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp;

public class SqliteAssetService : IAssetService
{
    private SQLiteAsyncConnection? _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_data.db3");

    private async Task Init()
    {
        if (_db is not null) return;

        _db = new SQLiteAsyncConnection(_dbPath);
        await _db.CreateTableAsync<Asset>();
        await _db.CreateTableAsync<User>();
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
        await Init();
        return await _db!.Table<Asset>().ToListAsync();
    }

    public async Task<int> SaveAssetAsync(Asset asset)
    {
        await Init();
        if (asset.Id != 0)
            return await _db!.UpdateAsync(asset);
        else
            return await _db!.InsertAsync(asset);
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        await Init();
        await _db!.DeleteAsync(asset);
    }

    public async Task<List<User>> GetUsersAsync()
    {
        await Init();
        return await _db!.Table<User>().ToListAsync();
    }

    public async Task<int> SaveUserAsync(User user)
    {
        await Init();
        if (user.Id != 0)
            return await _db!.UpdateAsync(user);
        else
            return await _db!.InsertAsync(user);
    }
}