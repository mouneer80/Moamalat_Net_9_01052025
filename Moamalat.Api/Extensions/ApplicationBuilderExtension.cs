//using BeneficiarySystem.Api.MiddleWares;

namespace Moamalat.API;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder App)
      => App.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
}
