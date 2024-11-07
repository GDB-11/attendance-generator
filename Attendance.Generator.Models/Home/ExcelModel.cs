namespace Attendance.Generator.Models.Home;

public sealed class ExcelModel
{
    public string? OutputFileName { get; set; }
    public MemoryStream? MainStudentFile { get; set; }
    public string? ProfessorName { get; set; }
    public string? AttendanceType { get; set; }
    public MemoryStream? SecondaryStudentFile { get; set; }
    public int Sessions { get; set; }
    public DateTime? Session1 { get; set; }
    public DateTime? Session2 { get; set; }
    public DateTime? Session3 { get; set; }
    public DateTime? Session4 { get; set; }
    public DateTime? Session5 { get; set; }
    public DateTime? Session6 { get; set; }
    public DateTime? Session7 { get; set; }
    public DateTime? Session8 { get; set; }
    public string? StudentsStatus { get; set; }
    public string? Title { get; set; }
}