namespace FileConverter.CustomClient;

public class CustomHttpClient : ICustomHttpClient
{
    private readonly HttpClient _httpClient;

    public CustomHttpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("deflate, br");
        _httpClient.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
    }

    public async Task<string> GetHtmlContentAsync(string url)
    {
        try
        {
            return await _httpClient.GetStringAsync(url);
        }
        catch (HttpRequestException)
        {
            return string.Empty;
        }
    }
    public async Task<byte[]> DownloadBytesAsync(Uri url)
    {
        try
        {
            return await _httpClient.GetByteArrayAsync(url);
        }
        catch (HttpRequestException)
        {
            return Array.Empty<byte>();
        }
    }
}