using ClosedXML.Excel;

namespace CvViewer.ApplicationServices.Extensions;

public static class XLWorksheetExtensions
{
    public static string? GetTextFromCellBelow(this IXLWorksheet worksheet, string searchTerm)
    {
        if (!worksheet
                .Search(searchTerm)
                .Single()
                .CellBelow()
                .TryGetValue<string>(out var value))
        {
            return null;
        }

        return value;
    }

    public static int GetColumnNumberByHeader(this IXLWorksheet worksheet, string header)
        => worksheet
            .Search(header)
            .Single()
            .WorksheetColumn()
            .ColumnNumber();

    public static string? GetTextByHeaderAndRowNumber(this IXLWorksheet worksheet, string header, int rowNumber)
    {
        if (!worksheet
            .Cell(rowNumber, worksheet.GetColumnNumberByHeader(header))
            .TryGetValue<string>(out var value))
        {
            return null;
        }

        return value;
    }

    public static int? GetNumberByHeaderAndRowNumber(this IXLWorksheet worksheet, string header, int rowNumber)
    {
        if (!worksheet
            .Cell(rowNumber, worksheet.GetColumnNumberByHeader(header))
            .TryGetValue<int>(out var value))
        {
            return null;
        }

        return value;
    }
}
