namespace TireShop.Configurations;

public record SimulationConfiguration(int CustomerCount, int MechanicCount) : IConfiguration
{
    public static string Name => "Simulation";
}
