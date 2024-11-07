using Application.Core.Interfaces;
using Attendance.Generator.Models.Home;
using Attendance.Generator.Models.Home.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Attendance.Generator.Components.Pages;

public partial class Home
{
    [Inject]
    public required IStudentAttendance StudentAttendanceService { get; init; }

    [Inject]
    public required IJSRuntime JSRuntime { get; init; }

    private bool isLegoSelected = false;

    private List<DateInputModel>? datesContainer;

    private readonly string minDate = DateTime.Now.ToString("yyyy-MM-dd");

    private int selectedSessions = 2;

    private ExcelModel model = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateDatesContainer(selectedSessions);
    }

    private void OnSessionChange(ChangeEventArgs e)
    {
        selectedSessions = int.Parse(e.Value.ToString());

        UpdateDatesContainer(selectedSessions);
    }

    private void UpdateDatesContainer(int numSessions)
    {
        datesContainer = [];

        for (int i = 0; i < numSessions; i++)
        {
            string className = numSessions == 2 ? "flex-1 min-w-[20%]" : "flex-1 min-w-[45%] md:min-w-[22%] 2xl:min-w-[10%]";

            datesContainer.Add(new DateInputModel { LabelText = $"Clase {i + 1}", ClassName = className });
        }
    }

    private void OnRadioChange(ChangeEventArgs e)
    {
        string? selectedValue = e.Value?.ToString();

        isLegoSelected = selectedValue == "Lego";
    }

    private async Task HandleClick()
    {
        Result<MemoryStream> stream = StudentAttendanceService.ProcessData(model.MapToAttendanceModel());

        if (stream.IsFailed)
        {
            return;
        }

        byte[] buffer = stream.Value.ToArray();

        string base64 = Convert.ToBase64String(buffer);

        await JSRuntime.InvokeVoidAsync("downloadFile", $"{model.OutputFileName}.xlsx", base64);
    }

    private async Task UploadMainStudentFile(InputFileChangeEventArgs args)
    {
        IBrowserFile file = args.File;

        using var stream = new MemoryStream();

        await file.OpenReadStream().CopyToAsync(stream);

        model.MainStudentFile = stream;
    }

    private async Task UploadComparativeStudentFile(InputFileChangeEventArgs args)
    {
        IBrowserFile file = args.File;

        using var stream = new MemoryStream();

        await file.OpenReadStream().CopyToAsync(stream);

        model.SecondaryStudentFile = stream;
    }
}