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

    public async Task<ServiceResponse<List<Asset>>> GetAssetsAsync()
    {
        try
        {
            await Init();
            var data = await _db!.Table<Asset>().ToListAsync();
            return ServiceResponse<List<Asset>>.Ok(data);
        }
        catch (Exception ex)
        {
            return ServiceResponse<List<Asset>>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResponse<int>> SaveAssetAsync(Asset asset)
    {
        try
        {
            await Init();
            int result = asset.Id != 0 ? await _db!.UpdateAsync(asset) : await _db!.InsertAsync(asset);
            return ServiceResponse<int>.Ok(result);
        }
        catch (Exception ex)
        {
            return ServiceResponse<int>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResponse<bool>> DeleteAssetAsync(Asset asset)
    {
        try
        {
            await Init();
            int rows = await _db!.DeleteAsync(asset);
            return ServiceResponse<bool>.Ok(rows > 0);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResponse<List<User>>> GetUsersAsync()
    {
        try
        {
            await Init();
            var data = await _db!.Table<User>().ToListAsync();
            return ServiceResponse<List<User>>.Ok(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving users: {ex.Message}");
            return ServiceResponse<List<User>>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResponse<int>> SaveUserAsync(User user)
    {
        try
        {
            await Init();
            int result = user.Id != 0 ? await _db!.UpdateAsync(user) : await _db!.InsertAsync(user);
            return ServiceResponse<int>.Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving user: {ex.Message}");
            return ServiceResponse<int>.Fail(ex.Message);
        }
    }
}