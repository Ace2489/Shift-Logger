namespace shift_logger;

public class ViewShiftDTO
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Duration
    {
        get
        {
            double roundedTime = Math.Round((EndTime - StartTime).Duration().TotalHours, 2);
            string[] times = roundedTime.ToString().Split('.');
            string hours = times[0];
            hours = hours.Length == 2 ? hours : 0 + hours; //pad with zeros
            if (times.Length == 2)
            {
                double rawMinutes = int.Parse(times[1]) * 60 / 100;
                string minutes = rawMinutes.ToString().Length == 2 ? rawMinutes.ToString() : 0 + rawMinutes.ToString(); // Pad with zeros as necessary
                return $"{hours}h:{minutes}m";
            }
            return $"{hours}h:00m";
        }
    }

}