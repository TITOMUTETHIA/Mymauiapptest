using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.JSInterop;
using MyMauiApp.Shared.Models;

namespace MyMauiApp.Shared.Services;

public class CartService : ICartService
{
    private readonly IJSRuntime _js;
    private List<Asset> _items = new();
    private const string StorageKey = "code_champs_cart";

    public CartService(IJSRuntime js)
    {
        _js = js;
    }

    public IReadOnlyList<Asset> Items => _items.AsReadOnly();

    public event Action? OnChange;

    public async Task InitializeAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                _items = JsonSerializer.Deserialize<List<Asset>>(json) ?? new();
                NotifyStateChanged();
            }
        }
        catch { /* Handle potential JS interop initialization delays */ }
    }

    public async Task AddToCartAsync(Asset asset)
    {
        _items.Add(asset);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(_items));
        NotifyStateChanged();
    }

    public async Task RemoveFromCartAsync(Asset asset)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.Id == asset.Id);
        if (itemToRemove != null && _items.Remove(itemToRemove))
        {
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(_items));
            NotifyStateChanged();
        }
    }

    public int GetCount() => _items.Count;

    private void NotifyStateChanged() => OnChange?.Invoke();
}