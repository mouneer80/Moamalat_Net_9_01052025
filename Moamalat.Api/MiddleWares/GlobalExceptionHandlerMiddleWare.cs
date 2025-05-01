using System;
using System.Text.Json;
using Moamalat.Entities;

namespace Moamalat.API
{
    public class GlobalExceptionHandlerMiddleWare : ExceptionHandlerOptions
    {
        public RequestDelegate _next { get; set; }
        public GlobalExceptionHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //var Cert=context.RequestServices.GetRequiredService<ICertificateValidationService>();
                await _next(context);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;

            var exceptionType = exception.GetType();

            //if(exceptionType==typeof(BadHttpRequestException))
            //{
            //    response.StatusCode = (int)StatusCodes.Status400BadRequest;
            //}
            //else if(exceptionType==typeof(UnauthorizedAccessException)|| exceptionType == typeof(KeyNotFoundException))
            //{
            //    response.StatusCode = (int)StatusCodes.Status401Unauthorized;
            //}
            //else if(exceptionType==typeof(NotImplementedException))
            //{
            //    response.StatusCode = (int)StatusCodes.Status501NotImplemented;
            //}
            //else if (exceptionType == typeof(NotSupportedException))
            //{
            //    response.StatusCode = (int)StatusCodes.Status505HttpVersionNotsupported;
            //}
            //else 
            //{
            //    response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            //}

            ApiResponse _ApiResponse = new ApiResponse()
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                Source = exception.Source,
                Succeeded = false

            };

            await response.WriteAsync(JsonSerializer.Serialize<ApiResponse>(_ApiResponse));
        }

    }
}

