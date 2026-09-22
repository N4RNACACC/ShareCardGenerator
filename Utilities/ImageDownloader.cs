using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShareCardGenerator.Utilities;

public static class ImageDownloader
{
    private static readonly string BasePath = AppContext.BaseDirectory;

    private static readonly HttpClient HttpClient = new HttpClient();

    public static async Task<string> DownloadWithHeadersAsync(string pid , string number , bool isSingleImgMode)
    {
        var savePath = Path.Combine(BasePath, "Cache", "BackgroundImages", pid+ "-" + number + ".jpg");

        // 检查缓存目录
        if (!File.Exists(savePath))
        {
            Directory.CreateDirectory(Path.Combine(BasePath, "Cache", "BackgroundImages"));
        }
        else
        {
            return savePath;
        }

        string url ;
        // 构建URL
        if (isSingleImgMode)
        {
            url = $"https://pixiv.re/{pid}.jpg";
        }
        else
        {
            url = $"https://pixiv.re/{pid}-{number}.jpg";
        }

        // 初始化Http请求
        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        // 模拟User-Agent
        request.Headers.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:150.0) Gecko/20100101 Firefox/150.0");

        using var response = await HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var bytes = await response.Content.ReadAsByteArrayAsync();
        // 保存背景图
        await File.WriteAllBytesAsync(savePath, bytes);

        // 返回保存路径
        return savePath;
    }
}