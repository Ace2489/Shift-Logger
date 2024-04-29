using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using InputHelpers;

namespace UI;

public class UIApp
{
    readonly GetTypesFromTerminal getTypes;

    readonly HttpClient client;

    readonly IOptionPicker option;

    readonly IDateTimeInput dateTimeHelper;
    public UIApp(string url, IOptionPicker optionPicker, IDateTimeInput dtInput)
    {
        getTypes = new();
        client = new()
        {
            BaseAddress = new Uri(url)
        };
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        option = optionPicker;
        dateTimeHelper = dtInput;
    }

    public async Task Run()
    {

        string operation = option.GetOption();
        if (operation.Equals("e"))
        {
            return;
        }
        else if (operation.Equals("c"))
        {
            Console.WriteLine("Getting the start time\n");
            DateTime startTime = dateTimeHelper.GetDateTime();
            Console.WriteLine("Getting the end time\n");
            DateTime endTime = dateTimeHelper.GetDateTime();
            AddShift shift = new() { StartTime = startTime, EndTime = endTime };
            ShiftRecord record = await PostShiftAsync(shift);
            Console.WriteLine("Record added successfully.");
        }
        else if (operation.Equals("v"))
        {
            List<ShiftRecord> shiftRecords = await GetShiftsAsync();
            if (shiftRecords.Count > 0)
            {
                Console.WriteLine("Id\tStartTime\t\tEndTime\t\t\tDuration\n");
                shiftRecords.ForEach(e => Console.WriteLine($"{e.Id}\t{e.StartTime:dd-mm-yyyy HH:mm:ss}\t{e.EndTime:dd-mm-yyyy HH:mm:ss}\t{e.Duration}"));
            }
            else
            {
                Console.WriteLine("No records returned.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid operation entered.");
        }

        client.Dispose();
    }

    public async Task<List<ShiftRecord>> GetShiftsAsync()
    {
        try
        {
            Stream jsonStream = await client.GetStreamAsync(client.BaseAddress);
            List<ShiftRecord>? shifts = await JsonSerializer.DeserializeAsync<List<ShiftRecord>>(jsonStream);
            return shifts ?? Enumerable.Empty<ShiftRecord>().ToList();

        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error with the request");
            Console.WriteLine(e.Message);
            return Enumerable.Empty<ShiftRecord>().ToList();
        }
    }

    public async Task<ShiftRecord> PostShiftAsync(AddShift shift)
    {
        try
        {

            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, shift);
            Stream jsonStream = await response.Content.ReadAsStreamAsync();
            ShiftRecord record = (await JsonSerializer.DeserializeAsync<ShiftRecord>(jsonStream))!;
            return record;

        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong with the request");
            Console.WriteLine(e.Message);
            return new ShiftRecord(Id: 0, StartTime: DateTime.MinValue, EndTime: DateTime.MinValue, Duration: "");
        }
    }

}
