using Blogging.System.Business.Logic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Blogging.System.WebApi.Middlewares {
    public class ExceptionHandlingMiddleware {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                _logger.LogError(ex.Message, ex);   
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = ex is AuthorNotFoundException ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = ex is AuthorNotFoundException ? "Resource not found" : "Unexpected error",
                    Detail = ex.Message,
                    Status = context.Response.StatusCode,
                    Instance = context.Request.Path
                });
            }
        }
    }
}
