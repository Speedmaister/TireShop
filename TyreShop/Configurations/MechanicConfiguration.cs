namespace TireShop.Configurations;

public record MechanicConfiguration(int PollingIntervalInMilliseconds) : IConfiguration
{
    public static string Name => "Mechanic";
}
