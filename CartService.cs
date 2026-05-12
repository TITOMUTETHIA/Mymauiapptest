using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MyMauiApp.Shared.Services;

public class CartService : ICartService
{
    private readonly IJSRuntime _js;
    private List<string> _items = new();
    private const string StorageKey = "code_champs_cart";

    public CartService(IJSRuntime js)
    {
        _js = js;
    }

    public IReadOnlyList<string> Items => _items.AsReadOnly();

    public event Action? OnChange;

    public async Task InitializeAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                _items = JsonSerializer.Deserialize<List<string>>(json) ?? new();
                NotifyStateChanged();
            }
        }
        catch { /* Handle potential JS interop initialization delays */ }
    }

    public async Task AddToCartAsync(string assetName)
    {
        _items.Add(assetName);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(_items));
        NotifyStateChanged();
    }

    public async Task RemoveFromCartAsync(string assetName)
    {
        if (_items.Remove(assetName))
        {
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(_items));
            NotifyStateChanged();
        }
    }

    public int GetCount() => _items.Count;

    private void NotifyStateChanged() => OnChange?.Invoke();
}