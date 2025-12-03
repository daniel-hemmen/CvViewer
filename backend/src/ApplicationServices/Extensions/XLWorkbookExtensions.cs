using ClosedXML.Excel;

namespace CvViewer.ApplicationServices.Extensions;

public static class XLWorkbookExtensions
{
    extension(XLWorkbook workbook)
    {
        public IXLWorksheet MetaSheet => workbook.Worksheet("Meta");
        public IXLWorksheet AdresSheet => workbook.Worksheet("Adres");
        public IXLWorksheet AuteurSheet => workbook.Worksheet("Auteur");
        public IXLWorksheet ContactgegevensSheet => workbook.Worksheet("Contactgegevens");
        public IXLWorksheet CertificatenSheet => workbook.Worksheet("Certificaten");
        public IXLWorksheet OpleidingenSheet => workbook.Worksheet("Opleidingen");
        public IXLWorksheet VaardighedenSheet => workbook.Worksheet("Vaardigheden");
        public IXLWorksheet WerkervaringSheet => workbook.Worksheet("Werkervaring");
    }
}
