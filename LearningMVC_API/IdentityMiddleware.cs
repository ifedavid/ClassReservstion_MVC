using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC_API
{
    public class IdentityMiddleware
    {



public class MyIdentityMiddlewareOptions
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String PassWord { get; set; }

    }
    public class MyIdentityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MyIdentityMiddlewareOptions _options;
        public MyIdentityMiddleware(RequestDelegate next, IOptions<MyIdentityMiddlewareOptions> options)
        {
            _next = next;
                _options = options.Value;
        }

            
    }

}
}
