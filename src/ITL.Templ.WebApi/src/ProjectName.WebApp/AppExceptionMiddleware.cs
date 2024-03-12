using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectName.Contracts;
using ProjectName.Domain;
using ProjectName.Exceptions;

namespace ProjectName.WebApp
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AppExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public AppExceptionMiddleware(
            RequestDelegate next, 
            ILogger<AppExceptionMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
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
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (ProjectNameException ex)
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
            var message = $"{code}: {ex.Message}";
            var allowStackTrace = _configuration.GetValue<bool>("AllowStackTrace", false);

            _logger.LogError(ex, message, ex.StackTrace);

            var response = new ErrorDetails
            {
                Title = ex.Message,
                Status = (int)code,
            };

            if (allowStackTrace)
            {
                response.Errors["stacktrace"] = new[] { ex.StackTrace };
            }

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
