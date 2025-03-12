using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Utils;

namespace WebApplication1.Middleware
{
    public class LoginMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly string excludePath = "/login";
        
        public LoginMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(excludePath))
            {
                await _next(context);
                return;
            }
            var token = context.Request.Headers["Authorization"];
            int result = CacheUtils.cache.Get<int>(token);
            if (result <= 0) context.Response.StatusCode = 401;
            else CacheUtils.cache.Set(token, result,30);
            await _next(context);
        }
    }
}