using Practice.Data.Views;

namespace Practice.Services.Interfaces
{
    public interface IExcelService
    {
        Task<string> ConvertXlsxToCsvAsync(string xlsxFilePath, string worksheetName = "");
        Task<List<string>> GetWorksheetNamesAsync(string xlsxFilePath);
        Task<List<SessionDto>> ConvertXlsxToSessionDtoColAsync(string xlsxFilePath, string worksheetName = "");
    }
}