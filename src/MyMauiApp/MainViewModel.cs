using System.Collections.ObjectModel;
using MyMauiApp.Shared.Models;

namespace MyMauiApp.ViewModels; // Assuming this ViewModel is MAUI-specific

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; set; }

    public MainViewModel()
    {
        Assets = new ObservableCollection<Asset>
        {
            new Asset { Name = "Bitcoin", Description = "The first cryptocurrency", Price = 64230.50m, ThumbnailUrl = "dotnet_bot.png" },
            new Asset { Name = "Ethereum", Description = "Smart contract platform", Price = 3450.20m, ThumbnailUrl = "dotnet_bot.png" },
            new Asset { Name = "Solana", Description = "High-speed blockchain", Price = 145.75m, ThumbnailUrl = "dotnet_bot.png" },
            new Asset { Name = "Cardano", Description = "Proof-of-stake platform", Price = 0.45m, ThumbnailUrl = "dotnet_bot.png" },
            new Asset { Name = "Polkadot", Description = "Multi-chain network", Price = 7.10m, ThumbnailUrl = "dotnet_bot.png" }
        };
    }
}