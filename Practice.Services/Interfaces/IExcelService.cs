using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface IExcelService
    {
        Task<List<string>> GetWorksheetNamesAsync(string xlsxFilePath);

        Task<List<SessionDto>> ConvertXlsxToSessionDtoColAsync(string xlsxFilePath, string worksheetName = "");
    }
}