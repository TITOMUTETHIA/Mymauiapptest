using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyMauiApp.Shared.Models;

namespace MyMauiApp.Shared.Services;

public interface ICartService
{
    IReadOnlyList<Asset> Items { get; }
    event Action? OnChange;
    Task AddToCartAsync(Asset asset);
    Task RemoveFromCartAsync(Asset asset);
    int GetCount();
    Task InitializeAsync();
}