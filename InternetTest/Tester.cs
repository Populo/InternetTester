﻿using System.Net;
using System.Net.NetworkInformation;

Console.WriteLine($"[{DateTime.Now}] Starting...");
Console.ForegroundColor = ConsoleColor.Red;

byte i = 0;
while (true)
{
    var outside = new Destination
    {
        Name = "Google",
        Address = IPAddress.Parse("142.250.217.174"),
        WarningTime = 400
    };
    var modem = new Destination
    {
        Name = "Modem",
        Address = IPAddress.Parse("10.0.0.1"),
        WarningTime = 100
    };
    var router = new Destination
    {
        Name = "Router",
        Address = IPAddress.Parse("192.168.68.1"),
        WarningTime = 100
    };
    
    Test(outside);
    Test(modem);
    Test(router);

    ++i;
    if (i == byte.MaxValue)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{DateTime.Now}] Still running...");
        Console.ForegroundColor = ConsoleColor.Red;
        i = 0;
    }
    Thread.Sleep(1000);
}

void Test(Destination dest)
{
    var ping = new Ping().Send(dest.Address);

    if (ping.Status != IPStatus.Success)
    {
        Console.WriteLine($"[{DateTime.Now}] {dest.Name} - Ping failed");
        dest.Failed = true;
    }
    else if (ping.RoundtripTime > dest.WarningTime)
    {
        Console.WriteLine($"[{DateTime.Now}] {dest.Name} - Slow ping {ping.RoundtripTime}ms");
        dest.Failed = true;
    }
    else
    {
        if (!dest.Failed) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Back to normal.");
        Console.ForegroundColor = ConsoleColor.Red;
        dest.Failed = false;
    }
}

internal class Destination
{
    public required string Name { get; init; }
    public required IPAddress Address { get; init; }
    public required int WarningTime { get; init; }
    public bool Failed { get; set; } = false;
}