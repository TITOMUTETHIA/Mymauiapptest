using Microsoft.AspNetCore.Components.Authorization;
using MyMauiApp.Models;
using System.Security.Claims;

namespace MyMauiApp.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthenticationService _authenticationService;

    public CustomAuthenticationStateProvider(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        // Subscribe to changes in the AuthenticationService to update Blazor's state
        _authenticationService.UserChanged += NotifyAuthenticationStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        if (_authenticationService.CurrentUser != null)
        {
            // Create claims based on the current user's username and role
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, _authenticationService.CurrentUser.Username),
                new Claim(ClaimTypes.Role, _authenticationService.CurrentUser.Role.ToString())
            }, "Custom authentication");
        }
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    private void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}