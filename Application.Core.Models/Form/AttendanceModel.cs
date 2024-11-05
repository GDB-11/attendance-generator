using Application.Core.Models.Constants;

namespace Application.Core.Models.Form;

public sealed record AttendanceModel
{
    public required string OutputFileName { get; init; }
    public required string MainStudentFile { get; init; }
    public required string ProfessorName { get; init; }
    public AttendanceType Type { get; init; }
    public string? SecondaryStudentFile { get; init; }
    public int Sessions { get; init; }
    public required DateTime Session1 { get; init; }
    public required DateTime Session2 { get; init; }
    public DateTime? Session3 { get; init; }
    public DateTime? Session4 { get; init; }
    public DateTime? Session5 { get; init; }
    public DateTime? Session6 { get; init; }
    public DateTime? Session7 { get; init; }
    public DateTime? Session8 { get; init; }
    public StudentsStatus? StudentsStatus { get; init; }
}