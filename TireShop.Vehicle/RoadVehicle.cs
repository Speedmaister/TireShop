namespace TireShop.Vehicle;

public abstract class RoadVehicle : Vehicle
{
    protected Tire[] tires;

    protected RoadVehicle(Manufacturer manufacturer)
        : base(manufacturer)
    {
        tires = new Tire[DefaultNumberOfTires];
        for (int i = 0; i < DefaultNumberOfTires; i++)
        {
            tires[i] = new SummerTire();
        }
    }

    public abstract int DefaultNumberOfTires { get; }

    public Type GetTiresType() => tires.FirstOrDefault().GetType();

    public void SwitchTires(Tire[] newTires)
    {
        if (newTires.Length != DefaultNumberOfTires)
        {
            throw new ArgumentException($"Not enough tires for this road vehicle. It should have {DefaultNumberOfTires} tires.");
        }

        tires = newTires;
    }
}
