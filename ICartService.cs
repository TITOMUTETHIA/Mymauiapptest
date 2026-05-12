using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMauiApp.Shared.Services;

public interface ICartService
{
    IReadOnlyList<string> Items { get; }
    event Action? OnChange;
    Task AddToCartAsync(string assetName);
    Task RemoveFromCartAsync(string assetName);
    int GetCount();
    Task InitializeAsync();
}