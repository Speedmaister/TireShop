namespace TireShop;

public interface IStorage
{
    Tire RetrieveSummerTire(float pressure, float maxTemperature);
    Tire RetrieveWinterTire(float pressure, float minTemperature, float thickness);
}