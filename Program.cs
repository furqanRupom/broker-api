
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5190");
builder.Services.AddHttpLogging((o) => { });
builder.Services.AddControllers();
builder.Services.AddTransient<IMyService, MyService>(); // AddSingleton,AddScoped, AddTransient
var app = builder.Build();

app.UseHttpLogging();
app.MapControllers();

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("Frist Middleware");
    await next.Invoke();
});


app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("Second Middleware");
    await next.Invoke();
});

app.MapGet("/", () => "Hello World");

app.MapGet("/hello", (IMyService myService) =>
{
    myService.LogCreation("Hello! Im from hello routes");
    return Results.Ok("Check the console");
});


app.Run();

// Depedency Injection in dotent

public interface IMyService
{
    void LogCreation(string message);
}


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
