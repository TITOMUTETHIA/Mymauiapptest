using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAssetService _assetService;
    public User? CurrentUser { get; private set; }
    public event Action? UserChanged;

    public AuthenticationService(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public async Task<bool> LoginAsync(string username)
    {
        var users = await _assetService.GetUsersAsync();
        var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (user != null)
        {
            CurrentUser = user;
            UserChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void Logout()
    {
        CurrentUser = null;
        UserChanged?.Invoke();
    }

    public bool IsInRole(UserRole requiredRole)
    {
        return CurrentUser != null && CurrentUser.Role >= requiredRole;
    }
}