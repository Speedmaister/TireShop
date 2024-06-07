namespace TireShop;

// Storage for tires. Possible to have a limited capacity by adding collections.
// Also can be made only as a collection holder and the mechanic or new class of storage worker to retrieve the tires from it.
// Needs to be singleton.
public class Storage : IStorage
{
    public Tire RetrieveSummerTire(float pressure, float maxTemperature)
        => new SummerTire(pressure, maxTemperature);

    public Tire RetrieveWinterTire(float pressure, float minTemperature, float thickness)
        => new WinterTire(pressure, minTemperature, thickness);
}
