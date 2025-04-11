using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_blog_service
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-API-KEY";
        private const string VALID_API_KEY = "my_secret_key";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKey) || apiKey != VALID_API_KEY)
            {
                context.Response.StatusCode = 401;
                return;
            }
            await _next(context);
        }

    }
}