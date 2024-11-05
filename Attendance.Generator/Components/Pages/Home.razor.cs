using Application.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;

namespace Attendance.Generator.Components.Pages;

public partial class Home
{
    [Inject]
    public required IStudentAttendance StudentAttendanceService { get; init; }

    [Inject]
    public required IJSRuntime JSRuntime { get; init; }

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