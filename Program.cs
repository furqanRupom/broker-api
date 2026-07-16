using System.Text.Json;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5190");
builder.Services.AddHttpLogging((o) => { });
builder.Services.AddControllers();
builder.Services.AddSingleton<IMyService, MyService>(); // AddSingleton,AddScoped, AddTransient
var app = builder.Build();

app.UseHttpLogging();
app.MapControllers();

// app.Use(
// async (context, next) =>
//     {
//         var myService = context.RequestServices.GetRequiredService<IMyService>();
//         myService.LogCreation("Frist Middleware");
//         await next.Invoke();
//     }
// );


// app.Use(
// async (context, next) =>
//     {
//         var myService = context.RequestServices.GetRequiredService<IMyService>();
//         myService.LogCreation("Second Middleware");
//         await next.Invoke();
//     }
// );

// app.Use(
// async (context, next) =>
//     {
//         var myService = context.RequestServices.GetRequiredService<IMyService>();
//         myService.LogCreation("Third Middleware");
//         await next.Invoke();
//     }
// );
app.MapGet("/", () => "Hello World");

// app.MapGet("/hello",
// (IMyService myService) =>
//     {
//         myService.LogCreation("Hello! Im from hello routes");
//         return Results.Ok("Check the console");
//     }
// );

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

// Derializer  - Json Derializer

app.MapPost("/d/auto", (Person PersonFromClient) =>
{
    return TypedResults.Ok(PersonFromClient);
});

app.MapPost("/d/json", async (HttpContext context) =>
{
    var person = await context.Request.ReadFromJsonAsync<Person>();
    return TypedResults.Json(person);
});

app.MapPost("/d/custom-options", async (HttpContext context) =>
{
    var options = new JsonSerializerOptions
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };
    var person = await context.Request.ReadFromJsonAsync<Person>(options);
    return TypedResults.Json(person);
});




app.Run();

public class Person
{
    public string? FullName { get; set; }
    public string? Email {get; set;}
    public string Password {get; set;}
    public bool IsActive { get; set; }
}


