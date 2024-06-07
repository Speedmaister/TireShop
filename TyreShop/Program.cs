using Newtonsoft.Json.Linq;
using TireShop.Configurations;

namespace TireShop;

public class Program
{
    private const string CarVehicleType = "car";
    private const string MotorcycleVehicleType = "motorcycle";

    public static ILogger log = new Logger();

    static void Main(string[] args)
    {
        var config = File.ReadAllText("config.json");
        var jsonConfig = JObject.Parse(config);
        var manufacturers = jsonConfig["Manufacturers"]
                                .ToObject<Dictionary<string, string[]>>()
                                .ToList();
        var mechanicConfig = jsonConfig[MechanicConfiguration.Name]
                                .ToObject<MechanicConfiguration>();
        var simulationConfig = jsonConfig[SimulationConfiguration.Name]
                                .ToObject<SimulationConfiguration>();

        var randomGenerator = new Random();

        var storage = new Storage();
        WorkerDispatcher dispatcher = new(simulationConfig.MechanicCount, storage, mechanicConfig);
        dispatcher.Start();

        List<Customer> customers = new();
        for (int i = 0; i < simulationConfig.CustomerCount; i++)
        {
            // wait for random delay
            Thread.Sleep(randomGenerator.Next(0, 1000));

            // Randomly select vehicle manufacturer
            var randomPair = manufacturers[randomGenerator.Next(0, manufacturers.Count - 1)];
            Manufacturer manufacturer = new Manufacturer(randomPair.Key);
            // Randomly select the according vehicle type
            string vehicleType = randomPair.Value[randomGenerator.Next(0, randomPair.Value.Length - 1)];
            Customer customer = null;
            if (vehicleType == CarVehicleType)
            {
                customer = new Customer(i + 1, new Car(manufacturer));
                log.Info($"Customer {customer.number} has arrived. Driving a car from {manufacturer.Name}");
            }
            else if (vehicleType == MotorcycleVehicleType)
            {
                customer = new Customer(i + 1, new Motorcycle(manufacturer));
                log.Info($"Customer {customer.number} has arrived. Driving a motorcycle from {manufacturer.Name}");
            }

            customers.Add(customer);
            dispatcher.AddCustomerToWaitQueue(customer);
        }

        dispatcher.StartCompletingWork();

        dispatcher.IsWorkCompleted().Wait();

        log.Info("Closing the tire shop. Thank you for your visit!");
    }
}
