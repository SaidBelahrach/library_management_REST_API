using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler(options =>
            {
                options.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                        var ex = context.Features.Get<IExceptionHandlerPathFeature>();
                        if (ex != null)
                        {
                            ErrorModel error = new ErrorModel((int)StatusCodes.Status500InternalServerError, ex.Error.Message, "", ex.Error.StackTrace);
                            await context.Response.WriteAsJsonAsync(error);
                        }
                    }
                );
            });
    }
}