namespace Attendance.Generator.Helper;

public static class ExcelHelper
{
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
}