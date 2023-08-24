namespace FileConverter.Convert;

public interface IConverter
{
    Task ConvertToCsvFIle(string excelFilePath);
}