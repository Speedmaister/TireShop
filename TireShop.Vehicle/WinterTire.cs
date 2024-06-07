namespace TireShop.Vehicle;

public class WinterTire : Tire
{
    private readonly float minTemperature;
    private readonly float thickness;

    public WinterTire(float pressure, float minTemperature, float thickness)
        : base(pressure)
    {
        this.minTemperature = minTemperature;
        this.thickness = thickness;
    }

    public override string GetProperties()
    {
        return $"Pressure: {pressure} bar, Minimum Temperature: {minTemperature}°C, Thickness: {thickness} mm";
    }
}
