using FileConverter.Convert;
using OfficeOpenXml;

namespace FileConverter.UnitTests;

[TestFixture]
public class ConverterTests
{
    private Converter converter;

    [SetUp]
    public void Setup()
    {
        converter = new Converter();
    }
    [Test]
    public async Task ConvertToCsvFile_ValidExcel_WritesCorrectCSV()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        // Arrange
        string excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WorldwideRigCounts.xlsx");
        string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WorldwideRigCounts.csv"); ;

        // Act
        await converter.ConvertToCsvFIle(excelFilePath);

        // Assert
        var csvLines = await File.ReadAllLinesAsync(csvFilePath);
        Assert.That(csvLines, Has.Length.EqualTo(30));
        Assert.Multiple(() =>
        {
            Assert.That(csvLines[0], Is.EqualTo(",2023,Latin America,Europe,Africa,Middle East,Asia Pacific,Total Intl.,Canada,U.S.,Total World"));
            Assert.That(csvLines[1], Is.EqualTo(",Jan,170,117,92,318,204,901,226,772,1899"));
        });
    }
}