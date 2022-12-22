using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectName.Domain;

namespace ProjectName.ConsumerApp
{
    public class ExceptionMiddleware
    {
        private static readonly string[] _allowedStackTraceEnvs = new[] { "Development", "Test" };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
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
                Extensions = new Dictionary<string, object>()
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (_allowedStackTraceEnvs.Contains(_environment.EnvironmentName))
            {
                response.Extensions["exceptionMessage"] = ex.Message;
                response.Extensions["stacktrace"] = ex.StackTrace;
            }

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}