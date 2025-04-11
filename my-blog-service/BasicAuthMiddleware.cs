using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_blog_service
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Response.StatusCode = 401;
                return;
            }
            var auth = authHeader.ToString().Split(" ");
            if(auth[0] != "Basic" || auth.Length != 2) return;
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(auth[1])).Split(":");
            var username = credentials[0];
            var password = credentials[1];
            if(username == "admin" && password == "password")
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }

        }

    }
}