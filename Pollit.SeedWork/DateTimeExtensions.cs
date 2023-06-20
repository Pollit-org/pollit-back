namespace Pollit.SeedWork;

public class DateTimeExtensions
{
    public static DateTime Min(params DateTime[] dateTimes)
    {
        return dateTimes.Min();
    }
    
    public static DateTime? Min(params DateTime?[] dateTimes)
    {
        return dateTimes.Min();
    }
    
    public static DateTime Max(params DateTime[] dateTimes)
    {
        return dateTimes.Max();
    }
    
    public static DateTime? Max(params DateTime?[] dateTimes)
    {
        return dateTimes.Max();
    }
}