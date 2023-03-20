using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        // RequestDelegate is a function that can process an HTTP request.
        
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        // Middleware Method
        // Pass in the HTTPContext - which encapsulates HTTP-specific information about an individual HTTP request.
        // Because this is Exception handling middleware we need to put it in a try catch block
        // When we created this we use RequestDelegate, a function that processes HTTP requests - called "next"

        // Needs "app.UseMiddleware<ExceptionMiddleware>();" adding at the top of middleware in program.cs
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // If there is no exception we want the middleware to move on to the next piece of middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Our own exception handling response
                // 
                _logger.LogError(ex, ex.Message); // Will output to console
                
                // Our own response - all of which are sent as json formatted
                context.Response.ContentType = "application/json";
                // Set the status code to be a 500
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Set the StackTrace to "Details" as defined in ApiException.cs
                var response = _env.IsDevelopment()
                    // If we are in development
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                    ex.StackTrace.ToString())
                    // If we are in production
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                // Format into json style
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
                
            }
        }
    }
}