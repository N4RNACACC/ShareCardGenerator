using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShareCardGenerator.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // Other ViewModels

    // Other properties and commands
    [ObservableProperty] public partial string IdInfo { get; set; } = idInfo();
    [ObservableProperty] public partial string SourceUrl { get; set; } = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SongInfo))]
    public partial string SongTitle { get; set; } = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SongInfo))]
    public partial string ArtistName { get; set; } = "";

    public string SongInfo => $"{SongTitle} - {ArtistName}";

    [ObservableProperty] public partial string AlbumName { get; set; } = "";

    [ObservableProperty] public partial string SongStyle { get; set; } = "";

    [RelayCommand] private static void Generate()
    {

    }

    [RelayCommand] private static void Save()
    {

    }

    private static string idInfo()
    {
        var time = DateTime.Now.ToString("yyyyMMdd");
        return $"随机推荐 #{time}";
    }


}