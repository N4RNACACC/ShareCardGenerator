using System;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShareCardGenerator.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    public string AppPath = AppContext.BaseDirectory;
    // Other ViewModels

    // Other properties and commands
    [ObservableProperty] public partial string IdInfo { get; set; } = idInfo();

    [ObservableProperty] public partial string InfoBar { get; set; } = "Ready";

    [ObservableProperty] public partial string BackgroundImage { get; set; }

    [ObservableProperty] public partial string SourceUrl { get; set; }

    [ObservableProperty] public partial string QrCodeData { get; set; }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SongInfo))] public partial string SongTitle { get; set; }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SongInfo))] public partial string ArtistName { get; set; }

    [ObservableProperty] public partial string AlbumName { get; set; }

    [ObservableProperty] public partial string SongStyle { get; set; }

    public string SongInfo => $"{SongTitle} - {ArtistName}";


    [RelayCommand] private void Generate()
    {
        var backgroundImageSavePath = Path.Combine(AppPath, "background");
        InfoBar = "Saving background image...";
        if (!Directory.Exists(backgroundImageSavePath))
        {
            Directory.CreateDirectory(backgroundImageSavePath);
        }



        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (string.IsNullOrEmpty(SourceUrl))
        {
            InfoBar = $"[{time}][ERROR] URL is not set or invalid.";
        }
        else
        {
            QrCodeData = SourceUrl;
            InfoBar = $"[{time}][INFO] QrCode generate successful.";
        }
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