using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using InputHelpers;

namespace UI;

public class UIApp
{
    readonly GetTypesFromTerminal getTypes;

    readonly HttpClient client;

    public UIApp(string url)
    {
        getTypes = new();
        client = new()
        {
            BaseAddress = new Uri(url)
        };
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            string operation = MainMenu().ToLower().Trim();
            if (operation.Equals("e"))
            {
                break;
            }
            else if (operation.Equals("c"))
            {
                DateTime startTime = GetDateTime();
                DateTime endTime = GetDateTime();
                AddShift shift = new() { StartTime = startTime, EndTime = endTime };
                ShiftRecord record = await PostShiftAsync(shift);
                Console.WriteLine("Record added successfully.");
                Console.ReadKey();
                continue;

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
                Console.WriteLine("Press any key to restart.");
                Console.ReadKey();
                continue;

            }
            else
            {
                Console.WriteLine("\nInvalid operation entered.\nPress any key to restart.");
                Console.ReadKey();
                continue;
            }

        }
        client.Dispose();

    }

    public string MainMenu()
    {
        Console.WriteLine("Welcome to the shifts logger.");
        Console.WriteLine("\nPick the letter that corresponds to your desired operation.");
        string choice = getTypes.GetString("(C)reate a shift record.\n(V)iew existing shift records.\n(E)xit the application\n");
        return choice;
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

        HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, shift);
        Stream jsonStream = await response.Content.ReadAsStreamAsync();
        ShiftRecord record = await JsonSerializer.DeserializeAsync<ShiftRecord>(jsonStream);
        return record;

    }

    public DateTime GetDateTime()
    {
        DateTime dateTime;
        int year, month, day;

        do
        {
            year = getTypes.GetInt("Enter a valid year (YYYY)");
        }
        while (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year || year > DateTime.Now.Year);

        do
        {
            month = getTypes.GetInt("Enter a valid month (MM)");
        }
        while (month < 1 || month > 12);

        int maxDays = DateTime.DaysInMonth(year, month);
        do
        {
            day = getTypes.GetInt("Enter a valid day (DD)");
        }
        while (day < 1 || day > maxDays);


        Console.WriteLine("Date: " + new DateTime(year, month, day).ToString("dd-MM-yyyy"));

        int hour, minutes, seconds;
        do
        {
            hour = getTypes.GetInt("Enter the hour of the day (24-hour time)");
        } while (hour < 0 || hour > 23);

        do
        {
            minutes = getTypes.GetInt("Enter the minutes(0-59)");
        } while (minutes < 0 || minutes > 59);
        do
        {
            seconds = getTypes.GetInt("Enter the seconds(0-59)");
        } while (seconds < 0 || seconds > 59);

        Console.WriteLine("Time: " + new TimeOnly(hour, minutes, seconds));
        dateTime = new DateTime(year, month, day, hour, minutes, seconds);
        return dateTime;
    }
}
