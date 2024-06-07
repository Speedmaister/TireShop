namespace TireShop.Vehicle;

public abstract class Vehicle
{
    protected Manufacturer manufacturer;

    protected Vehicle(Manufacturer manufacturer)
    {
        this.manufacturer = manufacturer;
    }

    public abstract string ShowInformation();
}
