//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Components.Authorization;
using Moamalat.Entities;
using Moamalat.Services;
using System.Net;
namespace Moamalat.Shared
{
    public static class ServicesInjection
    {


        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            string api = configuration.GetValue<string>("APIUrl");
            services.AddHttpClient<HttpClient>(
                "BaseHttpClient",
                client => client.BaseAddress = new Uri(api)
                ).AddHttpMessageHandler<HttpRequestHandler>();


            //-----services--------
            services.AddScoped<AddressService>();
            services.AddScoped<CorporateService>();
            services.AddScoped<DocumentService>();
            services.AddScoped<DocumentTypeService>();
            services.AddScoped<GradeService>();
            services.AddScoped<ParameterService>();
            services.AddScoped<PaymentDataService>();
            services.AddScoped<PaymentRequestService>();
            services.AddScoped<PersonService>();
            services.AddScoped<PersonCorporateService>();
            services.AddScoped<RegionService>();
            services.AddScoped<RequestDataService>();
            services.AddScoped<RequestDetailService>();
            services.AddScoped<RequestDocumentService>();
            services.AddScoped<RequestStatuService>();
            services.AddScoped<RequestTransactionService>();
            services.AddScoped<ServiceCategoryService>();
            services.AddScoped<ServiceDataService>();
            services.AddScoped<ServiceParameterService>();
            services.AddScoped<ServiceRequiredDocumentService>();
            services.AddScoped<TopUpHistoryService>();
            services.AddScoped<WilayatService>();

            services.AddSingleton<LoadingSpinnerService>();
            services.AddTransient<HttpRequestHandler>();

            return services;
        }
    }
}
