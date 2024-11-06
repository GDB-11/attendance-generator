using Application.Core.Interfaces;
using Attendance.Generator.Helper;
using ClosedXML.Excel;

namespace Application.Core.Services;

public sealed class ExcelHandlerService : IExcelHandler
{
    public MemoryStream CreateSpreadSheet()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Asistencia");

        #region Template preparation
        worksheet.SheetView.ZoomScale = 55;

        #region Header
        IXLRange headerMergedCell = worksheet.Range("A2:I3");
        headerMergedCell.Merge();

        headerMergedCell.Value = "Placeholder";

        headerMergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerMergedCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        headerMergedCell.Style.Font.FontSize = 20;
        headerMergedCell.Style.Font.Bold = true;
        #endregion

        #region Subheader
        IXLRange subheaderMergedCell = worksheet.Range("A4:I4");
        subheaderMergedCell.Merge();

        subheaderMergedCell.Value = "Seguimiento de la asistencia del estudiante";

        subheaderMergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subheaderMergedCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        subheaderMergedCell.Style.Font.FontSize = 20;
        subheaderMergedCell.Style.Font.Bold = true;
        #endregion

        #region Logo
        var logoPath = "wwwroot/img/utp-logo.png";
        var logo = worksheet.AddPicture(logoPath)
                            .MoveTo(worksheet.Cell("A1"), 5, 20)
                            .WithSize(235, 105);
        #endregion

        #region Caption table
        StyleAndMergeRange(worksheet, "J1:L1", "LEYENDA", bold: true, null, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "K2:L2", "ASISTIÓ", bold: true, null, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "K3:L3", "FALTÓ", bold: true, null, XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "K4:L4", "TARDANZA", bold: true, null, XLBorderStyleValues.Thin);

        StyleAndSetValue(worksheet, "J2", "A", bold: true, null, XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "J3", "F", bold: true, null, XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "J4", "T", bold: true, null, XLBorderStyleValues.Thin);
        #endregion

        #region Set column widths
        worksheet.Column(1).Width = ExcelHelper.PixelsToNoC(47);
        worksheet.Column(2).Width = ExcelHelper.PixelsToNoC(420);
        worksheet.Column(3).Width = ExcelHelper.PixelsToNoC(131);
        worksheet.Column(4).Width = ExcelHelper.PixelsToNoC(107);

        for (int i = 5; i <= 12; i++)
        {
            worksheet.Column(i).Width = ExcelHelper.PixelsToNoC(131);
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
        worksheet.Row(8).Height = ExcelHelper.PixelsToPoints(64);
        worksheet.Row(9).Height = ExcelHelper.PixelsToPoints(45);
        worksheet.Row(10).Height = ExcelHelper.PixelsToPoints(36);

        for (int i = 11; i <= 58; i++)
        {
            worksheet.Row(i).Height = ExcelHelper.PixelsToPoints(40);
        }
        #endregion

        #region Student column header
        StyleAndMergeRange(worksheet, "A7:A10", "No", bold: true, "#D9D9D9", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "B7:B10", "APELLIDOS Y NOMBRES", bold: true, "#D9D9D9", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "C7:C10", "Modalidad", bold: true, "#D9D9D9", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "D7:D10", "Estado", bold: true, "#D9D9D9", XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "E6", "", bold: true, "#404040");
        StyleAndSetValue(worksheet, "F6", "", bold: true, "#404040");
        StyleAndSetValue(worksheet, "G6", "", bold: true, "#404040");
        StyleAndSetValue(worksheet, "H6", "", bold: true, "#404040");
        StyleAndMergeRange(worksheet, "E7:F7", "01/01/1900", bold: true, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "G7:H7", "02/01/1900", bold: true, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "E8:F8", "Curso II (Placeholder)", bold: true, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "G8:H8", "Curso II (Placeholder)", bold: true, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "E9:F9", "Gianfranco Díaz (Placeholder)", bold: false, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndMergeRange(worksheet, "G9:H9", "Gianfranco Díaz (Placeholder)", bold: false, "#bfbfbf", XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "E10", "1°", bold: true, "#d9d9d9", XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "F10", "2°", bold: true, "#d9d9d9", XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "G10", "1°", bold: true, "#d9d9d9", XLBorderStyleValues.Thin);
        StyleAndSetValue(worksheet, "H10", "2°", bold: true, "#d9d9d9", XLBorderStyleValues.Thin);
        #endregion

        #region Set row border
        IXLRange contentRange = worksheet.Range("A11:H58");

        contentRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        contentRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        
        contentRange.Style.Font.FontSize = 14;
        #endregion
        #endregion

        foreach (var cell in worksheet.CellsUsed())
        {
            cell.Style.Font.FontName = "Arial"; // Set font to Arial
        }

        worksheet.SheetView.FreezeRows(10);
        worksheet.SheetView.FreezeColumns(2);

        // Save to MemoryStream instead of file
        var stream = new MemoryStream();
        workbook.SaveAs(stream);

        // Reset the position of the stream to the beginning
        stream.Position = 0;

        return stream;
    }

    public void StyleAndMergeRange(IXLWorksheet worksheet, string rangeAddress, string value, bool bold, string? hexColor = null, XLBorderStyleValues? outsideBorder = null)
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
        
        range.Style.Font.FontSize = 14;
        range.Style.Font.Bold = bold;

        if (!string.IsNullOrEmpty(hexColor))
        {
            range.Style.Fill.BackgroundColor = XLColor.FromHtml(hexColor);
        }
    }

    private void StyleAndSetValue(IXLWorksheet worksheet, string cellAddress, string value, bool bold, string? hexColor = null, XLBorderStyleValues? outsideBorder = null)
    {
        IXLCell cell = worksheet.Cell(cellAddress);
        cell.Value = value;

        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        if (outsideBorder is not null)
        {
            cell.Style.Border.OutsideBorder = outsideBorder.Value;
        }
        
        cell.Style.Font.FontSize = 14;
        cell.Style.Font.Bold = bold;

        if (!string.IsNullOrEmpty(hexColor))
        {
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml(hexColor);
        }
    }
}