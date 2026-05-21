using MyMauiApp.Shared.Models;
using System.Collections.ObjectModel;

namespace MyMauiApp.Shared.Services;

public class CartService : ICartService
{
    public ObservableCollection<Asset> CartItems { get; private set; }

    public CartService()
    {
        CartItems = new ObservableCollection<Asset>();
    }

    public void AddToCart(Asset asset)
    {
        // In a real app, you might want to check for duplicates and update quantity
        CartItems.Add(asset);
    }

    public void RemoveFromCart(Asset asset)
    {
        CartItems.Remove(asset);
    }

    public void ClearCart()
    {
        CartItems.Clear();
    }

    public decimal GetTotalPrice()
    {
        return CartItems.Sum(item => item.Price);
    }
}