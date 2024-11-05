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
                            .MoveTo(worksheet.Cell("A1"), 10, 10);
                            //.WithSize(100, 100); // Set the size as needed
        #endregion

        #region Caption
        IXLRange captionheaderMergedCell = worksheet.Range("J1:L1");
        captionheaderMergedCell.Merge();

        captionheaderMergedCell.Value = "LEYENDA";

        captionheaderMergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        captionheaderMergedCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        captionheaderMergedCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        captionheaderMergedCell.Style.Font.FontSize = 14;
        captionheaderMergedCell.Style.Font.Bold = true;
        #endregion

        //worksheet.Column(1).Width = 7.45;
        worksheet.Column(1).Width = ExcelHelper.CmToNoC(1.34);
        worksheet.Column(2).Width = ExcelHelper.CmToNoC(11.79);
        worksheet.Column(3).Width = ExcelHelper.CmToNoC(3.71);
        worksheet.Column(4).Width = ExcelHelper.CmToNoC(2.97);

        worksheet.Row(1).Height = ExcelHelper.ConvertCmToPoints(0.67);
        worksheet.Row(2).Height = ExcelHelper.ConvertCmToPoints(0.72);
        worksheet.Row(3).Height = ExcelHelper.ConvertCmToPoints(0.67);
        worksheet.Row(4).Height = ExcelHelper.ConvertCmToPoints(0.81);
        worksheet.Row(5).Height = ExcelHelper.ConvertCmToPoints(0.48);
        worksheet.Row(6).Height = ExcelHelper.ConvertCmToPoints(0.77);
        worksheet.Row(7).Height = ExcelHelper.ConvertCmToPoints(0.72);
        worksheet.Row(8).Height = ExcelHelper.ConvertCmToPoints(2.49);
        worksheet.Row(9).Height = ExcelHelper.ConvertCmToPoints(1.10);
        worksheet.Row(10).Height = ExcelHelper.ConvertCmToPoints(0.91);
        worksheet.Row(11).Height = ExcelHelper.ConvertCmToPoints(0.67);

        for (int i = 12; i <= 58; i++)
        {
            worksheet.Row(i).Height = ExcelHelper.ConvertCmToPoints(1.05);
        }        
        #endregion

        foreach (var cell in worksheet.CellsUsed())
        {
            cell.Style.Font.FontName = "Arial"; // Set font to Arial
        }

        // Save to MemoryStream instead of file
        var stream = new MemoryStream();
        workbook.SaveAs(stream);

        // Reset the position of the stream to the beginning
        stream.Position = 0;

        return stream;
    }
}