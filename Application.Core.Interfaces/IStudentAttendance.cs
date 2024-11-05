using Application.Core.Models.Form;
using FluentResults;

namespace Application.Core.Interfaces;

public interface IStudentAttendance
{
    Result<MemoryStream> ProcessData(AttendanceModel attendance);
}