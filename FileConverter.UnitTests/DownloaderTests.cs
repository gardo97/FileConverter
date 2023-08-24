using FileConverter.CustomClient;
using FileConverter.Download;
using Moq;

namespace FileConverter.UnitTests;

[TestFixture]
public class DownloaderTests
{
    private Mock<ICustomHttpClient> _mockHttpClient;
    private Downloader _downloader;

    [SetUp]
    public void Setup()
    {
        _mockHttpClient = new Mock<ICustomHttpClient>();
        _downloader = new Downloader(_mockHttpClient.Object);
    }

    [Test]
    public async Task DownloadExcelFile_ValidHtml_ReturnsFilePath()
    {
        // Arrange
        string url = "https://bakerhughesrigcount.gcs-web.com/intl-rig-count?c=79687&p=irol-rigcountsintl";
        string htmlContent = "<html><body><a href='/static-files/7240366e-61cc-4acb-89bf-86dc1a0dffe8'>\"Worldwide Rig Counts - Current & Historical Data\"</a></body></html>";
        string excelFilePath = "C:\\Users\\Korisnik\\Desktop\\FileConverter\\FileConverter.UnitTests\\bin\\Debug\\net6.0\\WorldwideRigCounts.xlsx";
        _mockHttpClient.Setup(mock => mock.GetHtmlContentAsync(It.IsAny<string>())).ReturnsAsync(htmlContent);
        _mockHttpClient.Setup(mock => mock.DownloadBytesAsync(It.IsAny<Uri>())).ReturnsAsync(new byte[0]);

        // Act
        string result = await _downloader.DownloadExcelFile(url);

        // Assert
        Assert.That(result, Is.EqualTo(excelFilePath));
    }

    [Test]
    public async Task DownloadExcelFile_InvalidHtml_ReturnsNull()
    {
        // Arrange
        string url = "https://bakerhughesrigcount.gcs-web.com/intl-rig-count?c=79687&p=irol-rigcountsintl";
        string htmlContent = "<html><body><a href='/invalid-link'>Link</a></body></html>";
        _mockHttpClient.Setup(mock => mock.GetHtmlContentAsync(It.IsAny<string>())).ReturnsAsync(htmlContent);

        // Act
        string result = await _downloader.DownloadExcelFile(url);

        // Assert
        Assert.That(result, Is.Empty);
    }
    [Test]
    public async Task DownloadExcelFile_Url_ReturnsNull()
    {
        // Arrange
        string url = "https://example.com/nonexistent";

        // Act
        string result = await _downloader.DownloadExcelFile(url);

        // Assert
        Assert.That(result, Is.Empty);
    }
}