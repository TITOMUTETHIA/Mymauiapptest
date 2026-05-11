using System.Collections.ObjectModel;
using MyMauiApp.Models;

namespace MyMauiApp.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Asset> Assets { get; set; }

    public MainViewModel()
    {
        // Initializing with sample data
        Assets = new ObservableCollection<Asset>
        {
            new Asset { Name = "Bitcoin", Description = "Digital Gold", Value = 65000, Icon = "btc.png" },
            new Asset { Name = "Ethereum", Description = "Smart Contracts", Value = 3500, Icon = "eth.png" },
            new Asset { Name = "Solana", Description = "High Performance", Value = 140, Icon = "sol.png" }
        };
    }
}