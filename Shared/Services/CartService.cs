using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMauiApp.Shared.Services
{
    public class CartService : ICartService {
        public List<string> Items { get; private set; } = new();
        public event Action OnChange;

        public Task InitializeAsync() {
            // Logic to load existing cart items can go here
            return Task.CompletedTask;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}