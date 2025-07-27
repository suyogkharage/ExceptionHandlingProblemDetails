using ExceptionHandlingProblemDetails.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExceptionHandlingProblemDetails.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception occurred.");

                var problemDetails = new ProblemDetails
                {
                    Title = "An unexpected error occurred",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                switch (ex)
                {
                    case BadRequestException:
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "Bad Request";
                        break;
                    case NotFoundException:
                        problemDetails.Status = StatusCodes.Status404NotFound;
                        problemDetails.Title = "Resource not found";
                        break;
                    default:
                        problemDetails.Status = StatusCodes.Status500InternalServerError;
                        problemDetails.Title = "Internal Server Error";
                        break;
                }

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status.Value;

                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        }
    }
}
