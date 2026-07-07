
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5190");
builder.Services.AddHttpLogging((o) => {});
builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpLogging();
app.MapControllers();

app.Use( async (context,next) => {
	await next.Invoke();
});


app.MapGet("/", () => "Hello World");

app.MapGet("/hello", () => "This is hello routes");


app.Run();

