using FileConverter.Convert;
using FileConverter.CustomClient;
using FileConverter.Download;

try
{
    string url = "https://bakerhughesrigcount.gcs-web.com/intl-rig-count?c=79687&p=irol-rigcountsintl";
    ICustomHttpClient customHttpClient = new CustomHttpClient();
    IDownloader downloader = new Downloader(customHttpClient);
    IConverter converter = new Converter();

    string excelFilePath = await downloader.DownloadExcelFile(url);

    await converter.ConvertToCsvFIle(excelFilePath);

    Console.WriteLine("CSV conversion completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}