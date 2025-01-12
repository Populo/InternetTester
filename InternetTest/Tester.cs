using System.Net;
using System.Net.NetworkInformation;

Console.WriteLine("Starting...");
Console.ForegroundColor = ConsoleColor.Red;
var p = new Ping();
bool failed = false;

byte i = 0;
while (true)
{
    // google
    var ping = p.Send(IPAddress.Parse("142.250.217.174"));

    if (ping.Status != IPStatus.Success)
    {
        Console.WriteLine($"Ping failed at {DateTime.Now}");
        failed = true;
    }
    else if (ping.RoundtripTime > 200)
    {
        Console.WriteLine($"Roundtrip time too long. Took {ping.RoundtripTime}ms at: {DateTime.Now}");
        failed = true;
    }
    else
    {
        if (failed)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Back to normal.");
            Console.ForegroundColor = ConsoleColor.Red;
            failed = false;
        }
    }

    ++i;
    if (i == byte.MaxValue)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Still running...");
        Console.ForegroundColor = ConsoleColor.Red;
        i = 0;
    }
    Thread.Sleep(1000);
}