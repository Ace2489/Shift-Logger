using System.Text.Json.Serialization;

namespace UI;

public record class ShiftRecord
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("endTime")] DateTime EndTime,
    [property: JsonPropertyName("duration")] string Duration

);

public record class AddShift
{
    public DateTime StartTime;
    public DateTime EndTime;
}

