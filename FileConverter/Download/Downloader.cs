using AngleSharp;
using AngleSharp.Dom;
using FileConverter.CustomClient;

namespace FileConverter.Download;
public class Downloader : IDownloader
{
    private readonly ICustomHttpClient _httpClient;
    private readonly string baseUrl = "https://bakerhughesrigcount.gcs-web.com"; // Base URL of the website
    private readonly string linkToFile = "Worldwide Rig Counts - Current & Historical Data";
    public Downloader(ICustomHttpClient _customHttpClient)
    {
        _httpClient = _customHttpClient;
    }
    public async Task<string> DownloadExcelFile(string url)
    {
        string htmlContent = await _httpClient.GetHtmlContentAsync(url);
        if (!string.IsNullOrEmpty(htmlContent))
        {
            IConfiguration config = Configuration.Default;
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(req => req.Content(htmlContent));

            string? excelLink = GetExcelLink(document);
            if (excelLink is null)
                return string.Empty;

            Uri absoluteUri = new(new Uri(baseUrl), excelLink);

            byte[] excelContent = await _httpClient.DownloadBytesAsync(absoluteUri);

            string excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WorldwideRigCounts.xlsx");
            await File.WriteAllBytesAsync(excelFilePath, excelContent);

            Console.WriteLine($"Excel file downloaded and saved to: {excelFilePath}");
            return excelFilePath;

        }
        Console.WriteLine($"Excel file isn't downloaded");
        return string.Empty;
    }

    private string? GetExcelLink(IDocument document)
    {
        IElement? excelLinkElement = document.QuerySelector($"a:contains('{linkToFile}')");
        return excelLinkElement?.GetAttribute("href");
    }
}