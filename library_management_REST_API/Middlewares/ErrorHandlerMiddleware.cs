using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async System.Threading.Tasks.Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            ErrorModel err = new ErrorModel(500, ex.Message, ex.GetType().ToString(), ex.StackTrace);
            switch (ex)
            {
                case UnauthorizedAccessException e:  // Unauthorized error
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    err.Status = (int)HttpStatusCode.Unauthorized;
                    break;
                case KeyNotFoundException e: // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    err.Status = (int)HttpStatusCode.NotFound;
                    break;
                default: // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    err.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            await context.Response.WriteAsJsonAsync(err);
        }
    }
}