using InputHelpers;
using UI;

namespace ui;

public class DateTimeInput : IDateTimeInput
{
    private readonly GetTypesFromTerminal getTypes = new();

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
