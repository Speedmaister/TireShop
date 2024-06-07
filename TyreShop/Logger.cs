using System.Diagnostics;

namespace TireShop;

internal class Logger : ILogger
{
    private Stopwatch _timer;

    public Logger()
    {
        _timer = new Stopwatch();
        _timer.Start();
    }

    public void Error(string message, Exception ex)
    {
        Console.WriteLine("{0,-10:F2}{1}. {2}", _timer.Elapsed.TotalSeconds, message, ex);
    }

    public void Info(string message)
    {
        Console.WriteLine("{0,-10:F2}{1}", _timer.Elapsed.TotalSeconds, message);
    }
}
