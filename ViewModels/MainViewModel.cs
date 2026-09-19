using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShareCardGenerator.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public partial string SourceUrl { get; set; } = "";


    [ObservableProperty] public partial string SongTitle { get; set; } = "";

    [ObservableProperty] public partial string ArtistName { get; set; } = "";

    [ObservableProperty] public partial string AlbumTitle { get; set; } = "";

    [ObservableProperty] public partial string SongStyle { get; set; } = "";

    [RelayCommand] private static void Generate()
    {

    }

    [RelayCommand] private static void Save()
    {

    }
}