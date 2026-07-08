using System;

public class MyService : IMyService
{
    private readonly int _serviceId;

    public MyService()
    {
        _serviceId = new Random().Next(10000 * 99999);
    }

    public void LogCreation(string message)
    {
        Console.WriteLine($"Messsage - {message}. Service ID - {_serviceId}");
    }
}
