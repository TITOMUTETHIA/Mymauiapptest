using System.Collections.ObjectModel;
using MyMauiApp.Models;

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; } = new()
    {
        new Asset { Name = "Bitcoin", Description = "The first cryptocurrency", Value = 64230.50m, Icon = "dotnet_bot.png" },
        new Asset { Name = "Ethereum", Description = "Smart contract platform", Value = 3450.20m, Icon = "dotnet_bot.png" },
        new Asset { Name = "Solana", Description = "High-speed blockchain", Value = 145.75m, Icon = "dotnet_bot.png" },
        new Asset { Name = "Cardano", Description = "Proof-of-stake platform", Value = 0.45m, Icon = "dotnet_bot.png" },
        new Asset { Name = "Polkadot", Description = "Multi-chain network", Value = 7.10m, Icon = "dotnet_bot.png" }
    };
}