namespace FileConverter.Download;
public interface IDownloader
{
    Task<string> DownloadExcelFile(string url);
}