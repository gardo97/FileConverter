using OfficeOpenXml;

namespace FileConverter.Convert;

public class Converter : IConverter
{
    public async Task ConvertToCsvFIle(string excelFilePath)
    {
        if (!string.IsNullOrEmpty(excelFilePath))
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath));
            if (package.Workbook.Worksheets.Count > 0)
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WorldwideRigCounts.csv");
                using StreamWriter csvWriter = new StreamWriter(csvFilePath);

                int lastRow = worksheet.Dimension.End.Row;
                int lastColumn = worksheet.Dimension.End.Column;

                int currentYear = DateTime.Now.Year;
                int lastYear = currentYear - 1;

                bool includeData = false;

                for (int row = 1; row <= lastRow; row++)
                {
                    string yearValue = worksheet.Cells[row, 2].Text;

                    if (int.TryParse(yearValue, out int year))
                        includeData = year == lastYear || year == currentYear;

                    if (includeData)
                    {
                        string rowData = "";
                        for (int col = 1; col <= lastColumn - 1; col++)
                        {
                            string cellValue = worksheet.Cells[row, col].Text;

                            rowData += col != lastColumn - 1 ? cellValue + "," : cellValue;
                        }
                        await csvWriter.WriteLineAsync(rowData);
                    }
                }
                Console.WriteLine("CSV file created successfully.");
            }
        }
        else
            Console.WriteLine("CSV file not created.");
    }
}