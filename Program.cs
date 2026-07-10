using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5190");
builder.Services.AddHttpLogging((o) => { });
builder.Services.AddControllers();
builder.Services.AddSingleton<IMyService, MyService>(); // AddSingleton,AddScoped, AddTransient
var app = builder.Build();

app.UseHttpLogging();
app.MapControllers();

app.Use(
async (context, next) =>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();
        myService.LogCreation("Frist Middleware");
        await next.Invoke();
    }
);


app.Use(
async (context, next) =>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();
        myService.LogCreation("Second Middleware");
        await next.Invoke();
    }
);

app.Use(
async (context, next) =>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();
        myService.LogCreation("Third Middleware");
        await next.Invoke();
    }
);
app.MapGet("/", () => "Hello World");

app.MapGet("/hello",
(IMyService myService) =>
    {
        myService.LogCreation("Hello! Im from hello routes");
        return Results.Ok("Check the console");
    }
);

// Serialization - Json Serializar

var product = new Product { Name = "Apple Phone", Description = "Nice Apple Phone", Price = 12.22 };

app.MapGet("/manual-json", () =>
{
    var jsonString = JsonSerializer.Serialize(product);
    return TypedResults.Text(jsonString, "application/json");
});

app.MapGet("/json", () =>
{
    return TypedResults.Json(product);
});

app.MapGet("/auto", () =>
{
    return product;
});

app.Run();




