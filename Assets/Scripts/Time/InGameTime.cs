public class InGameTime
{
    public static int Day { get; set; } = 0;
    public static int Hour { get; set; } = 0;
    public static int Minute { get; set; } = 0;
    public static float Second { get; set; } = 0;

    public InGameTime(int day, int hour, int minute, float second)
    {
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
    }
}
