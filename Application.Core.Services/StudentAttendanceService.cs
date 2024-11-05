using Application.Core.Interfaces;
using Application.Core.Models.Form;
using FluentResults;

namespace Application.Core.Services;

public sealed class StudentAttendanceService : IStudentAttendance
{
    private readonly IExcelHandler _excelHandler;

    public StudentAttendanceService(IExcelHandler excelHandler)
    {
        _excelHandler = excelHandler;
    }

    public Result<MemoryStream> ProcessData(AttendanceModel attendance)
    {
        MemoryStream file = _excelHandler.CreateSpreadSheet();

        return Result.Ok(file);
    }
}