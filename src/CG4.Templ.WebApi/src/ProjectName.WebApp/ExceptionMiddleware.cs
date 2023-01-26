using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectName.Core.Domain;

namespace ProjectName.WebApp
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {
            string message = $"{code}: {ex.Message}";
            _logger.LogError(ex, message, ex.StackTrace);

            var response = new ErrorResponse(ex.Message)
            {
                Extensions = new Dictionary<string, object>
                {
                    ["stacktrace"] = ex.StackTrace,
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonSerializer.Serialize(
                response,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }));
        }
    }
}
