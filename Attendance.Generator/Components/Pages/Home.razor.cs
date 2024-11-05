using Application.Core.Interfaces;
using Attendance.Generator.Models.Home;
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

    private string minDate = DateTime.Now.ToString("yyyy-MM-dd");

    private int selectedSessions = 2;

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
        datesContainer = new List<DateInputModel>();

        for (int i = 0; i < numSessions; i++)
        {
            var className = numSessions == 2 ? "flex-1 min-w-[20%]" : "flex-1 min-w-[45%] md:min-w-[22%] 2xl:min-w-[10%]";
            datesContainer.Add(new DateInputModel { LabelText = $"Clase {i + 1}", ClassName = className });
        }
    }

    private void OnRadioChange(ChangeEventArgs e)
    {
        var selectedValue = e.Value.ToString();
        isLegoSelected = selectedValue == "Lego";
    }

    private async void HandleClick()
    {
        // Generate the Excel file as a MemoryStream
        var stream = StudentAttendanceService.ProcessData(null);

        if (stream.IsFailed)
        {
            return;
        }

        // Create a byte array from the MemoryStream
        var buffer = stream.Value.ToArray();

        // Use JavaScript interop to trigger the download in the browser
        var base64 = Convert.ToBase64String(buffer);

        await JSRuntime.InvokeVoidAsync("downloadFile", "Asistencia.xlsx", base64);
    }

    private async Task UploadImages(InputFileChangeEventArgs args)
    {
        try
        {
            //var currentTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            //var path = Path.Combine(_webHostEnvironment.WebRootPath, $@"UserData/dummy/UploadTest/{currentTime}");
            //Directory.CreateDirectory(path);
            var file = args.File;
            /*foreach (var file in files)
            {
                var filePath = Path.Combine(path, file.Name);
                await using FileStream fs = new(filePath, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(fs);
            }*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}