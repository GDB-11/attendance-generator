using Application.Core.Models.Constants;
using Application.Core.Models.Form;

namespace Attendance.Generator.Models.Home.Extensions;

public static class Mapper
{
    public static AttendanceModel MapToAttendanceModel(this ExcelModel obj)
    {
        return new AttendanceModel
        {
            MainStudentFile = obj.AttendanceType,
            ProfessorName = obj.ProfessorName,
            Type = obj.AttendanceType == "1" ? AttendanceType.Normal : AttendanceType.LEGO,
            SecondaryStudentFile = obj.SecondaryStudentFile,
            Sessions = obj.Sessions,
            Session1 = obj.Session1.Value,
            Session2 = obj.Session2.Value,
            Session3 = obj.Session3,
            Session4 = obj.Session4,
            Session5 = obj.Session5,
            Session6 = obj.Session6,
            Session7 = obj.Session7,
            Session8 = obj.Session8,
            StudentsStatus = obj.StudentsStatus == "1" ? StudentsStatus.Inscrito : StudentsStatus.Baja,
            Title = obj.Title
        };
    }
}