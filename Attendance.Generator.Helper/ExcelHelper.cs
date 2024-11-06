namespace Attendance.Generator.Helper;

public static class ExcelHelper
{
    public const string DarkGray = "#404040";
    public const string Gray = "#bfbfbf";
    public const string LightGray = "#D9D9D9";

    public static double PixelsToNoC(int pixels)
    {
        const double pixelPadding = 5.0;
        const double pixelsPerNoC = 7.0;

        return (pixels - pixelPadding) / pixelsPerNoC;
    }

    public static double PixelsToPoints(int pixels)
    {
        const double pointsPerInch = 72.0;
        const double pixelsPerInch = 96.0;

        return (pixels * pointsPerInch) / pixelsPerInch;
    }

    public static string GetColumnLetter(int columnNumber)
    {
        string columnLetter = string.Empty;

        while (columnNumber > 0)
        {
            int modulo = (columnNumber - 1) % 26;
            columnLetter = Convert.ToChar(65 + modulo) + columnLetter;
            columnNumber = (columnNumber - modulo) / 26;
        }

        return columnLetter;
    }
}