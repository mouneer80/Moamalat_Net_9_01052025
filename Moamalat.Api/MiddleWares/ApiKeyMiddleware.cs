using Microsoft.Extensions.DependencyInjection;
using Moamalat.Data;

namespace Moamalat.API
{
    public class ApiKeyMiddleware
    {


        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER_NAME = "PPS-API-KEY";
        //private readonly ApiKeyRepository apiKeyRepository;
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
          //  apiKeyRepository = _apiKeyRepository;
        }

        public async Task Invoke(HttpContext context, IServiceScopeFactory serviceScopeFactory)
        {

           
            string expectedApiKey=string.Empty;

            // Check if API Key is present in the request headers
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var providedApiKey))
            {
                
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key is missing");
                return;
            }
            using (var scope = serviceScopeFactory.CreateScope())  // Create a new DI scope
            {
               // var apiKeyRepository = scope.ServiceProvider.GetRequiredService<ApiKeyRepository>();
                
                   // var apiKey = await apiKeyRepository.GetApiKeyByValue(providedApiKey);
                   // if (apiKey == null)
                   // {
                   //     context.Response.StatusCode = 401; // Unauthorized
                   //     await context.Response.WriteAsync("API Key is invalid");

                       
                  //      return;
                  //  }
                   // else
                  //  {
                        //expectedApiKey = apiKey.ApiKeyValue;
                  //  }
                
               

                
            }
            // Validate API Key
            if (!string.Equals(providedApiKey, expectedApiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            // Call the next middleware
            await _next(context);
        }

        
    }
}
