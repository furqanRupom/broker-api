using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5190");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World");

app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) =>
{
    return $"User ID : {userId}, Post Slug : {slug}";
});

app.MapGet("/products/{id:int:min(0)}", (int id) =>
{
    return $"Product ID : {id}";
});

app.MapGet("/report/{year?}",(int? year) => {
   return $"Report of the ${year}"; 
});

app.MapGet("/search",(string? q, int page=2) => {
   return $"Searching for q : {q} on page {2}"; 
});

app.MapGet("/search",(string? q, int page=2) => {
   return $"Searching for q : {q} on page {2}"; 
});


app.Run();

