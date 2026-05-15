using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMauiApp.Shared.Services
{
    public interface ICartService {
        List<string> Items { get; }
        event Action OnChange;
        Task InitializeAsync();
        void AddToCart(string item);
    }
}