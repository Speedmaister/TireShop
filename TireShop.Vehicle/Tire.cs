namespace TireShop.Vehicle
{
    public abstract class Tire
    {
        protected readonly float pressure;

        protected Tire(float pressure)
        {
            this.pressure = pressure;
        }

        public abstract string GetProperties();
    }
}
