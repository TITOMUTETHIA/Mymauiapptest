using SQLite;
using MyMauiApp.Models;

namespace MyMauiApp.Services;

public class SqliteAssetService : IAssetService
{
    private SQLiteAsyncConnection? _db;
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_data.db3");

    private async Task Init()
    {
        if (_db is not null) return;

        await _initializationSemaphore.WaitAsync();
        try
        {
            if (_db is not null) return;

            _db = new SQLiteAsyncConnection(_dbPath);
            await _db.CreateTablesAsync<Asset, User>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization failed: {ex.Message}");
            throw;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
        try
        {
            await Init();
            return await _db!.Table<Asset>().ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving assets: {ex.Message}");
            return new List<Asset>();
        }
    }

    public async Task<int> SaveAssetAsync(Asset asset)
    {
        try
        {
            await Init();
            if (asset.Id != 0)
                return await _db!.UpdateAsync(asset);
            else
                return await _db!.InsertAsync(asset);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving asset: {ex.Message}");
            return 0;
        }
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        try
        {
            await Init();
            await _db!.DeleteAsync(asset);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting asset: {ex.Message}");
        }
    }

    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            await Init();
            return await _db!.Table<User>().ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving users: {ex.Message}");
            return new List<User>();
        }
    }

    public async Task<int> SaveUserAsync(User user)
    {
        try
        {
            await Init();
            if (user.Id != 0)
                return await _db!.UpdateAsync(user);
            else
                return await _db!.InsertAsync(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving user: {ex.Message}");
            return 0;
        }
    }
}