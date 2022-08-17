using Core.Exceptions.Middleware;
using Core.Utilities.Result;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Core.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await handleExceptionAsync(httpContext, e);
            }
        }

        private async Task handleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal server error";

            if (e.GetType() == typeof(ValidationException))
            {
                IEnumerable<ValidationFailure> errors = ((ValidationException)e).Errors;
                message = string.Join("\n", errors.Select(x => x.ErrorMessage));
            }
            if (e.GetType() == typeof(UnauthorizedException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                message = e.Message;
            }
            IResult result = new ErrorResult(message);
            await httpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(result));
        }
    }
}
