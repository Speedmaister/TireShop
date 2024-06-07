using TireShop.Configurations;

namespace TireShop;

public class MechanicWorker
{
    private static Random randomTimerGenerator = new();

    private readonly IStorage _storage;
    private readonly MechanicConfiguration _mechanicConfiguration;
    private readonly Func<Customer> _getNextCustomer;
    private readonly Func<bool> _areCustomersWaiting;

    public MechanicWorker(IStorage storage,
                          MechanicConfiguration mechanicConfiguration,
                          Func<Customer> getNextCustomer,
                          Func<bool> areCustomersWaiting)
    {
        _storage = storage;
        _mechanicConfiguration = mechanicConfiguration;
        _getNextCustomer = getNextCustomer;
        _areCustomersWaiting = areCustomersWaiting;
    }

    public async Task DoWork(Task workDone)
    {
        // Mechanic checks if there are waiting customers on specific interval or if the work is done
        while (_areCustomersWaiting() || !workDone.IsCompleted)
        {
            try
            {
                var customer = _getNextCustomer();
                if (customer == null)
                {
                    await Task.Delay(_mechanicConfiguration.PollingIntervalInMilliseconds);
                }
                else
                {
                    // random delay for changing tires between 2 and 5 secs
                    var timeForChange = randomTimerGenerator.Next(2000, 5000);
                    Program.log.Info($"Customer {customer.number} car tires are being changed and it will take {timeForChange.ToSeconds()} seconds.");
                    await Task.Delay(timeForChange);
                    ChangeTires(customer);
                }
            }
            catch (Exception ex)
            {
                Program.log.Error("Terminating mechanic with error.", ex);
            }
        }
    }

    private void ChangeTires(Customer customer)
    {
        var oldTiresType = customer.vehicle.GetTiresType();
        List<Tire> newTires = new();
        // DefaultNumberOfTires can be added to Customer class to get a specific number of tires for each customer
        for (int i = 0; i < customer.vehicle.DefaultNumberOfTires; i++)
        {
            // switch old summer tires to new winter tires
            if (oldTiresType == typeof(SummerTire))
            {
                // Tire properties can be added to Customer class and passed here
                newTires.Add(_storage.RetrieveWinterTire(2.2f, -10, 0.2f));
            }
            // switch old winter tires to new summer tires
            else if (oldTiresType == typeof(WinterTire))
            {
                // Tire properties can be added to Customer class and passed here
                newTires.Add(_storage.RetrieveSummerTire(SummerTire.DefaultPressure, SummerTire.DefaultMaximumTemperature));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        customer.vehicle.SwitchTires(newTires.ToArray());
        Program.log.Info($"Customer {customer.number} has left.");
    }
}
