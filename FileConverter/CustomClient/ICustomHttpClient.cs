namespace FileConverter.CustomClient;

public interface ICustomHttpClient
{
    Task<string> GetHtmlContentAsync(string url);
    Task<byte[]> DownloadBytesAsync(Uri url);
}