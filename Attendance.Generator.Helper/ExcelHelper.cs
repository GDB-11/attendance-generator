namespace Attendance.Generator.Helper;

public static class ExcelHelper
{
    public static double NoCConstant = 0.2;

    public static double CmToNoC(double cm)
    {
        return cm / NoCConstant;
    }

    public static double ConvertCmToPoints(double cm)
    {
        return cm * (72 / 2.54);
    }
}