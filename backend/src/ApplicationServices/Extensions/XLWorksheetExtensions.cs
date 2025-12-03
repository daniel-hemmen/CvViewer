using ClosedXML.Excel;

namespace CvViewer.ApplicationServices.Extensions;

public static class XLWorksheetExtensions
{
    public static string GetTextFromCellBelow(this IXLWorksheet worksheet, string searchTerm)
        => worksheet
            .Search(searchTerm)
            .Single()
            .CellBelow()
            .GetText();

    public static int GetColumnNumberByHeader(this IXLWorksheet worksheet, string header)
        => worksheet
            .Search(header)
            .Single()
            .WorksheetColumn()
            .ColumnNumber();

    public static string GetTextByHeaderAndRowNumber(this IXLWorksheet worksheet, string header, int rowNumber)
        => worksheet
            .Cell(rowNumber, worksheet.GetColumnNumberByHeader(header))
            .GetText();

    public static int? GetNumberByHeaderAndRowNumber(this IXLWorksheet worksheet, string header, int rowNumber)
        => int.TryParse(GetTextByHeaderAndRowNumber(worksheet, header, rowNumber), out var result)
        ? result
        : null;
}
