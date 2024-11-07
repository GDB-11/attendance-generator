using Application.Core.Interfaces;
using Attendance.Generator.Helper;
using ClosedXML.Excel;

namespace Application.Core.Services;

public sealed class ExcelHandlerService : IExcelHandler
{
    public MemoryStream CreateSpreadSheet(int sessions)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Asistencia");

        #region Template preparation
        worksheet.SheetView.ZoomScale = 55;

        #region Headers
        if (sessions < 8)
        {
            StyleAndMergeRange(worksheet, "A2:I3", "Placeholder", bold: true, fontSize: 20, wrapText: false);
            StyleAndMergeRange(worksheet, "A4:I4", "Seguimiento de la asistencia del estudiante", bold: true, fontSize: 20, wrapText: false);
        }
        else
        {
            StyleAndMergeRange(worksheet, "A2:Q3", "Placeholder", bold: true, fontSize: 20, wrapText: false);
            StyleAndMergeRange(worksheet, "A4:Q4", "Seguimiento de la asistencia del estudiante", bold: true, fontSize: 20, wrapText: false);
        }
        #endregion

        #region Logo
        var logoPath = "wwwroot/img/utp-logo.png";
        var logo = worksheet.AddPicture(logoPath)
                            .MoveTo(worksheet.Cell("A1"), 5, 20)
                            .WithSize(235, 105);
        #endregion

        #region Caption table
        if (sessions < 8)
        {
            StyleAndMergeRange(worksheet, "J1:L1", "LEYENDA", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "K2:L2", "ASISTIÓ", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "K3:L3", "FALTÓ", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "K4:L4", "TARDANZA", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);

            StyleAndSetValue(worksheet, "J2", "A", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, "J3", "F", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, "J4", "T", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
        }
        else
        {
            StyleAndMergeRange(worksheet, "R1:T1", "LEYENDA", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "S2:T2", "ASISTIÓ", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "S3:T3", "FALTÓ", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, "S4:T4", "TARDANZA", bold: true, fontSize: 14, wrapText: false, hexColor: null, XLBorderStyleValues.Thin);

            StyleAndSetValue(worksheet, "R2", "A", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, "R3", "F", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, "R4", "T", bold: true, fontSize: 14, hexColor: null, XLBorderStyleValues.Thin);
        }
        #endregion

        #region Set column widths
        worksheet.Column(1).Width = ExcelHelper.PixelsToNoC(47);
        worksheet.Column(2).Width = ExcelHelper.PixelsToNoC(420);
        worksheet.Column(3).Width = ExcelHelper.PixelsToNoC(131);
        worksheet.Column(4).Width = ExcelHelper.PixelsToNoC(107);

        int startColumn = 5;
        int endColumn = sessions < 8 ? 12 : 20;
        double width = sessions < 8 ? ExcelHelper.PixelsToNoC(131) : ExcelHelper.PixelsToNoC(89);

        for (int i = startColumn; i <= endColumn; i++)
        {
            worksheet.Column(i).Width = width;
        }
        #endregion

        #region Set row heights
        worksheet.Row(1).Height = ExcelHelper.PixelsToPoints(27);
        worksheet.Row(2).Height = ExcelHelper.PixelsToPoints(27);
        worksheet.Row(3).Height = ExcelHelper.PixelsToPoints(27);
        worksheet.Row(4).Height = ExcelHelper.PixelsToPoints(35);
        worksheet.Row(5).Height = ExcelHelper.PixelsToPoints(18);
        worksheet.Row(6).Height = ExcelHelper.PixelsToPoints(31);
        worksheet.Row(7).Height = ExcelHelper.PixelsToPoints(31);
        worksheet.Row(8).Height = sessions < 8 ? ExcelHelper.PixelsToPoints(64) : ExcelHelper.PixelsToPoints(96);
        worksheet.Row(9).Height = ExcelHelper.PixelsToPoints(45);
        worksheet.Row(10).Height = ExcelHelper.PixelsToPoints(36);

        for (int i = 11; i <= 58; i++)
        {
            worksheet.Row(i).Height = ExcelHelper.PixelsToPoints(40);
        }
        #endregion

        #region Student column header
        StyleAndMergeRange(worksheet, "A7:A10", "No", bold: true, fontSize: 14, wrapText: false, ExcelHelper.LightGray, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "B7:B10", "APELLIDOS Y NOMBRES", bold: true, fontSize: 14, wrapText: false, ExcelHelper.LightGray, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "C7:C10", "Modalidad", bold: true, fontSize: 14, wrapText: false, ExcelHelper.LightGray, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "D7:D10", "Estado", bold: true, fontSize: 14, wrapText: false, ExcelHelper.LightGray, XLBorderStyleValues.Thin);

        for (int i = 0; i < sessions; i++)
        {
            int baseColumn = 5 + i * 2;
            string col1 = ExcelHelper.GetColumnLetter(baseColumn);
            string col2 = ExcelHelper.GetColumnLetter(baseColumn + 1);
            string col3 = ExcelHelper.GetColumnLetter(baseColumn + 2);
            string col4 = ExcelHelper.GetColumnLetter(baseColumn + 3);

            StyleAndSetValue(worksheet, $"{col1}6", string.Empty, bold: true, fontSize: 14, ExcelHelper.DarkGray);
            StyleAndSetValue(worksheet, $"{col2}6", string.Empty, bold: true, fontSize: 14, ExcelHelper.DarkGray);
            StyleAndMergeRange(worksheet, $"{col1}7:{col2}7", "01/01/1900", bold: true, fontSize: 14, wrapText: false, ExcelHelper.Gray, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, $"{col1}8:{col2}8", "Curso II (Placeholder)", bold: true, fontSize: 14, wrapText: true, ExcelHelper.Gray, XLBorderStyleValues.Thin);
            StyleAndMergeRange(worksheet, $"{col1}9:{col2}9", "Gianfranco Díaz (Placeholder)", bold: false, fontSize: 14, wrapText: true, ExcelHelper.Gray, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, $"{col1}10", "1°", bold: true, fontSize: 14, ExcelHelper.LightGray, XLBorderStyleValues.Thin);
            StyleAndSetValue(worksheet, $"{col2}10", "2°", bold: true, fontSize: 14, ExcelHelper.LightGray, XLBorderStyleValues.Thin);
            StyleRange(worksheet, $"{col1}11:{col1}58", bold: false, fontSize: 11, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
            StyleRange(worksheet, $"{col2}11:{col2}58", bold: false, fontSize: 11, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
        }
        #endregion

        #region Set content styles
        StyleRange(worksheet, "A11:A58", bold: true, fontSize: 16, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
        StyleRange(worksheet, "B11:B58", bold: false, fontSize: 15, XLAlignmentHorizontalValues.Left, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
        StyleRange(worksheet, "C11:C58", bold: false, fontSize: 15, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
        StyleRange(worksheet, "D11:D58", bold: false, fontSize: 15, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLBorderStyleValues.Thin);
        #endregion
        #endregion

        foreach (var cell in worksheet.CellsUsed())
        {
            cell.Style.Font.FontName = "Arial";
        }

        worksheet.SheetView.FreezeRows(10);
        worksheet.SheetView.FreezeColumns(2);

        var stream = new MemoryStream();
        workbook.SaveAs(stream);

        stream.Position = 0;

        return stream;
    }

    public void StyleAndMergeRange(IXLWorksheet worksheet, string rangeAddress, string value, bool bold, double fontSize, bool wrapText, string? hexColor = null, XLBorderStyleValues? outsideBorder = null)
    {
        IXLRange range = worksheet.Range(rangeAddress);
        range.Merge();

        range.Value = value;

        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        if (outsideBorder is not null)
        {
            range.Style.Border.OutsideBorder = outsideBorder.Value;
        }
        
        range.Style.Font.FontSize = fontSize;
        range.Style.Font.Bold = bold;
        range.Style.Alignment.WrapText = wrapText;

        if (!string.IsNullOrEmpty(hexColor))
        {
            range.Style.Fill.BackgroundColor = XLColor.FromHtml(hexColor);
        }
    }

    public void StyleRange(IXLWorksheet worksheet, string rangeAddress, bool bold, double fontSize, XLAlignmentHorizontalValues horizontalAlignment, XLAlignmentVerticalValues verticalAlignment, XLBorderStyleValues? border = null)
    {
        IXLRange range = worksheet.Range(rangeAddress);

        range.Style.Alignment.Horizontal = horizontalAlignment;
        range.Style.Alignment.Vertical = verticalAlignment;

        if (border is not null)
        {
            range.Style.Border.OutsideBorder = border.Value;
            range.Style.Border.InsideBorder = border.Value;
        }

        range.Style.Font.FontSize = fontSize;
        range.Style.Font.Bold = bold;
        range.Style.Font.FontName = "Arial";
    }

    private void StyleAndSetValue(IXLWorksheet worksheet, string cellAddress, string value, bool bold, double fontSize, string? hexColor = null, XLBorderStyleValues? outsideBorder = null)
    {
        IXLCell cell = worksheet.Cell(cellAddress);
        cell.Value = value;

        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        if (outsideBorder is not null)
        {
            cell.Style.Border.OutsideBorder = outsideBorder.Value;
        }
        
        cell.Style.Font.FontSize = fontSize;
        cell.Style.Font.Bold = bold;

        if (!string.IsNullOrEmpty(hexColor))
        {
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml(hexColor);
        }
    }
}