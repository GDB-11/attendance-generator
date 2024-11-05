using Microsoft.AspNetCore.Components;

namespace Attendance.Generator.Components.Shared.Form;

public partial class InputWithIcon
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public required string Id { get; set; }

    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public string? PlaceholderText { get; set; }

    [Parameter]
    public string Type { get; set; } = "text";

    [Parameter]
    public bool IsRequired { get; set; } = false;

    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private async Task HandleInput(string newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(Value);
    }
}