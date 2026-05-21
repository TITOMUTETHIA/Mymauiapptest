using MyMauiApp.Shared.Models;
using System.Collections.ObjectModel;

namespace MyMauiApp.Shared.Services;

public interface ICartService
{
    ObservableCollection<Asset> CartItems { get; }

    void AddToCart(Asset asset);
    void RemoveFromCart(Asset asset);
    void ClearCart();
    decimal GetTotalPrice();
}