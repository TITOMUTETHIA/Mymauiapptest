using MyMauiApp.Models;

namespace MyMauiApp.Services;

public interface IAuthenticationService
{
    User? CurrentUser { get; }
    event Action? UserChanged;
    Task<bool> LoginAsync(string username);
    void Logout();
    bool IsInRole(UserRole requiredRole);
}