using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;



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
	public MyIdentityMiddleware(RequestDelegate next, MyIdentityMiddlewareOptions)
	{
        _next = next;
	}
}
