using System.Text;

namespace TireShop.Vehicle
{
    public class Car : RoadVehicle
    {
        public Car(Manufacturer manufacturer)
            : base(manufacturer)
        {
        }

        public override int DefaultNumberOfTires => 4;

        public override string ShowInformation()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Driving a car from {manufacturer.Name}.");
            for (int i = 0; i < tires.Length; i++)
            {
                sb.AppendLine($"Using {tires[i].GetType().Name} with properties: {tires[i].GetProperties()}");
            }

            return sb.ToString();
        }
    }
}
