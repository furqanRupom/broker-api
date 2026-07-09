using Microsoft.AspNetCore.Mvc;
using System;


// For Error Handling

[Route("/api/[Controller]")]
[ApiController]
public class ErrorHandlingController : ControllerBase
{
    [HttpGet("division")]

    public IActionResult GetDivisionResult(int numerator, int denominator)
    {
        try
        {
            var result = numerator / denominator;
            return Ok($"Here's the result : {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Division by zero not allowed");
            return BadRequest("Cannot divide by zero");
        }
    }
}
