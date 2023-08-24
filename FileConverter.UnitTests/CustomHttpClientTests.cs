using FileConverter.CustomClient;

namespace FileConverter.UnitTests;

[TestFixture]
public class CustomHttpClientTests
{
    private ICustomHttpClient _customHttpClient;

    [SetUp]
    public void Setup()
    {
        _customHttpClient = new CustomHttpClient();
    }

    [Test]
    public async Task GetHtmlContentAsync_ValidUrl_ReturnsHtmlContent()
    {
        // Arrange
        string url = "https://bakerhughesrigcount.gcs-web.com/intl-rig-count?c=79687&p=irol-rigcountsintl";

        // Act
        string htmlContent = await _customHttpClient.GetHtmlContentAsync(url);

        // Assert
        Assert.That(htmlContent, Is.Not.Null);
        Assert.That(htmlContent, Does.Contain("<!DOCTYPE html>")); // Assuming HTML content starts with <!DOCTYPE html>
    }
    [Test]
    public async Task GetHtmlContentAsync_InValidUrl_ReturnsEmptyString()
    {
        // Arrange
        string url = "https://example.com/nonexistent";

        // Act
        string htmlContent = await _customHttpClient.GetHtmlContentAsync(url);

        // Assert
        Assert.That(htmlContent, Is.Not.Null);
        Assert.That(htmlContent, Is.Empty);
    }
    [Test]
    public async Task DownloadBytesAsync_ValidUrl_ReturnsByteArray()
    {
        // Arrange
        string baseUrl = "https://bakerhughesrigcount.gcs-web.com/static-files/7240366e-61cc-4acb-89bf-86dc1a0dffe8";

        Uri url = new(baseUrl);

        // Act
        byte[] bytes = await _customHttpClient.DownloadBytesAsync(url);

        // Assert
        Assert.That(bytes, Is.Not.Null);
        Assert.That(bytes, Is.Not.Empty);
    }

    [Test]
    public async Task DownloadBytesAsync_InvalidUrl_ReturnsNull()
    {
        // Arrange
        Uri url = new("https://example.com/nonexistent");

        // Act
        byte[] bytes = await _customHttpClient.DownloadBytesAsync(url);

        // Assert
        Assert.That(bytes, Is.Not.Null);
        Assert.That(bytes, Is.Empty);
    }
}