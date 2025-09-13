﻿namespace Blogging.System.WebApi.Middlewares {
    public static class ExceptionHandlingMiddlewareExtensions {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
