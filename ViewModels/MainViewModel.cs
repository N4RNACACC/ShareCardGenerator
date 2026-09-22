using System;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ShareCardGenerator.Utilities;
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

    [ObservableProperty] public partial IImage CardImage { get; set; }

    [ObservableProperty] public partial bool SingleImgMode { get; set; } = false; // 单图模式

    [ObservableProperty] public partial string BackgroundImageID { get; set; }

    [ObservableProperty] public partial string IDNumber { get; set; }

    [ObservableProperty] public partial string SourceUrl { get; set; }

    [ObservableProperty] public partial string QrCodeData { get; set; }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SongInfo))] public partial string SongTitle { get; set; }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SongInfo))] public partial string ArtistName { get; set; }

    [ObservableProperty] public partial string AlbumName { get; set; }

    [ObservableProperty] public partial string SongStyle { get; set; }

    public string SongInfo => $"{SongTitle} - {ArtistName}";

    public event System.EventHandler? SaveRequested;

    [RelayCommand] private void Save()
    {
        SaveRequested?.Invoke(this, EventArgs.Empty);
    }


    [RelayCommand] private async Task Generate()
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (string.IsNullOrEmpty(BackgroundImageID) || string.IsNullOrEmpty(SourceUrl))
        {
            InfoBar = $"[{time}][ERROR] Background image PID or URL is not set or invalid.";
            return;
        }

        InfoBar = $"[{time}][INFO] Downloading background image...";
        // 下载背景图并获取保存路径
        var savePath =  await ImageDownloader.DownloadWithHeadersAsync(BackgroundImageID, IDNumber, SingleImgMode);

        // 设置卡片背景
        CardImage = new Bitmap(savePath);

        InfoBar = $"[{time}][INFO] Apply background image successful.";

        // 生成二维码
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

    private static string idInfo()
    {
        var time = DateTime.Now.ToString("yyyyMMdd");
        return $"随机推荐 #{time}";
    }


}