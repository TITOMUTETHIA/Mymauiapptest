using MyMauiApp.Models;

namespace MyMauiApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAssetService _assetService;
    public User? CurrentUser { get; private set; }
    public event Action? UserChanged;

    public AuthenticationService(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public async Task<ServiceResponse<bool>> LoginAsync(string username)
    {
        try
        {
            var response = await _assetService.GetUsersAsync();
            
            if (!response.Success)
            {
                return ServiceResponse<bool>.Fail(response.Message ?? "Failed to retrieve user data from the service.");
            }

            var user = response.Data?.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            
            if (user != null)
            {
                CurrentUser = user;
                UserChanged?.Invoke();
                return ServiceResponse<bool>.Ok(true);
            }
            return ServiceResponse<bool>.Fail("Invalid username. Please try again.");
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"A system error occurred: {ex.Message}");
        }
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