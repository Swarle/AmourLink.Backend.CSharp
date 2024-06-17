using System.Net;
using AmourLink.Infrastructure.ResponseHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AmourLink.Infrastructure.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly JsonSerializerSettings _serializerSettings = new ()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public ApiExceptionMiddleware(RequestDelegate next,
            ILogger<ApiExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleInternalServerError(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpException ex)
        {
            if (ex.StatusCode == HttpStatusCode.InternalServerError)
            {
                await HandleInternalServerError(context, ex);
            }
            else
            {
                _logger.LogError(ex, ex.ErrorMessages.ToString());
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) ex.StatusCode;

                var jsonResponse = JsonConvert.SerializeObject(ApiResponse.HttpException(ex),
                    _serializerSettings);
                
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        private async Task HandleInternalServerError(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment() || _env.IsEnvironment("Local")
                ? ApiResponse.Exception(ex)
                : new ApiResponse(ResponseType.HttpError, ex.Message);
            
            var json = JsonConvert.SerializeObject(response, _serializerSettings);

            await context.Response.WriteAsync(json);
        }
        
        
    }


    public static class ApiExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}