using OfficeOpenXml;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;
using System.Diagnostics;

namespace Practice.Services.Services
{
    public class ExcelService : IExcelService
    {

        public ExcelService()
        {
            // Nothing to initialize
        }

        public async Task<List<string>> GetWorksheetNamesAsync(string xlsxFilePath)
        {
            List<string> sheetNames = new List<string>();
            try
            {
                using var package = new ExcelPackage(new FileInfo(xlsxFilePath));
                var worksheets = package.Workbook.Worksheets
                    .Select(ws => ws.Name)
                    .ToList();

                foreach (var worksheet in worksheets)
                {
                    sheetNames.Add(worksheet);
                }

                return sheetNames;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting worksheets: {ex.Message}", ex);
            }
        }

        public Task<List<SessionDto>> ConvertXlsxToSessionDtoColAsync(string xlsxFilePath, string worksheetName = "")
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(xlsxFilePath));
                
                var worksheet = package.Workbook.Worksheets[worksheetName] ?? throw new ArgumentException("Worksheet not found");

                var range = new List<SessionDto>();

                var data = worksheet.Cells["A2:F125"];

                var rowCount = worksheet.Dimension?.Rows ?? 0;

                DateTime lastDate = new();

                // Process each row from 2 to end of data
                for (int row = 2; row <= rowCount; row++)
                {
                    if(worksheet.Cells[row, 1].Value == null 
                        && worksheet.Cells[row, 3].Value == null
                        && worksheet.Cells[row, 4].Value == null
                        && worksheet.Cells[row, 5].Value == null
                        )
                    {
                        continue;
                    }

                    var practiceDate = DateTime.MinValue;
                    try
                    {
                        Debug.Write($"Processing row {row}:");

                        var cell1 = worksheet.Cells[row, 1].Value;
                        if (cell1 != null && !string.IsNullOrWhiteSpace(cell1.ToString()))
                        {
                            practiceDate = cell1 switch
                            {
                                DateTime dt => dt,
                                double d => DateTime.FromOADate(d),
                                string s when DateTime.TryParse(s, out var date) => date,
                                _ => throw new InvalidDataException($"Cannot convert cell value to DateTime: {cell1}")
                            };

                            lastDate = practiceDate;
                        }
                        else
                        {
                            practiceDate = lastDate;
                        }

                        //Debug.WriteLine($"  practiceDate: {practiceDate}; ");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }

                    var cell3 = worksheet.Cells[row, 3].Value == null || string.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? string.Empty : worksheet.Cells[row, 3].Value.ToString();
                    //Debug.WriteLine($"  cell3: {cell3}; ");

                    var cell4 = worksheet.Cells[row, 4].Value == null || string.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? string.Empty : worksheet.Cells[row, 4].Value.ToString();
                    //Debug.WriteLine($"  cell4: {cell4}; ");

                    var cell5 = worksheet.Cells[row, 5].Value == null || string.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? string.Empty : worksheet.Cells[row, 5].Value.ToString();
                    //Debug.WriteLine($"  cell5: {cell5}; ");

                    var plog = new SessionDto();

                    plog.PracticeDate = practiceDate;

                    plog.DurationMinutes = int.Parse(cell3.ToString());

                    plog.Activity = cell4.ToString();

                    plog.Notes = cell5.ToString();

                    range.Add(plog);
                }

                Debug.WriteLine($"... Processing Complete.");

                return Task.FromResult(range);
            }
            catch
            {
                throw;
            }
        }
    }
}

