namespace TireShop.Vehicle;

public class SummerTire : Tire
{
    public const float DefaultMaximumTemperature = 50f;
    public const float DefaultPressure = 2.5f;

    private readonly float maxTemperature;

    public SummerTire()
        : base(DefaultPressure)
    {
        maxTemperature = DefaultMaximumTemperature;
    }

    public SummerTire(float pressure, float maxTemperature)
        : base(pressure)
    {
        this.maxTemperature = maxTemperature;
    }

    public override string GetProperties()
    {
        return $"Pressure: {pressure} bar, Maximum Temperature: {maxTemperature}°C";
    }
}
