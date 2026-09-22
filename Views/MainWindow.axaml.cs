using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ShareCardGenerator.ViewModels;

namespace ShareCardGenerator.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.SaveRequested += OnSaveRequested;
        }
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        var id = DateTime.Now.ToString("yyyyMMdd");
        // 找到 ShareCardView
        var cardView = this.FindControl<ShareCardView>("ShareCardView");
        if (cardView is null)
        {
            return;
        }

        // 让用户选择保存位置
        var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Card",
            SuggestedFileName = $"ShareCard_#{id}.png",
            FileTypeChoices =
            [
                new FilePickerFileType("PNG Image") { Patterns = ["*.png"] }
            ]
        });

        if (file is null) return;

        // 渲染并保存
        await using var stream = await file.OpenWriteAsync();
        RenderControlToPng(cardView, stream);

        if (DataContext is MainViewModel vm)
            vm.InfoBar = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][INFO] Card saved";
    }

    private static void RenderControlToPng(Control target, Stream output, double scale = 3.0)
    {
        var logicalSize = target.Bounds.Size;
        if (logicalSize.Width <= 0 || logicalSize.Height <= 0) return;

        // 先按逻辑尺寸完成布局
        target.Measure(logicalSize);
        target.Arrange(new Avalonia.Rect(logicalSize));

        // 关键：像素尺寸 = 逻辑尺寸 × 缩放比
        var pixelSize = new Avalonia.PixelSize(
            (int)Math.Ceiling(logicalSize.Width * scale),
            (int)Math.Ceiling(logicalSize.Height * scale));

        var dpi = new Avalonia.Vector(96 * scale, 96 * scale);

        using var bitmap = new RenderTargetBitmap(pixelSize, dpi);
        bitmap.Render(target);
        bitmap.Save(output);
    }
}