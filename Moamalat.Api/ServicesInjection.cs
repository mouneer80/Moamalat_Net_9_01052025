//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Components.Authorization;
using Moamalat.Data;

namespace Moamalat.Api
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {


            services.AddScoped<AddressRepository>();
            services.AddScoped<CorporateRepository>();
            services.AddScoped<DocumentRepository>();
            services.AddScoped<DocumentTypeRepository>();
            services.AddScoped<GradeRepository>();
            services.AddScoped<ParameterRepository>();
            services.AddScoped<PaymentDataRepository>();
            services.AddScoped<PaymentRequestRepository>();
            services.AddScoped<PersonRepository>();
            services.AddScoped<PersonCorporateRepository>();
            services.AddScoped<RegionRepository>();
            services.AddScoped<RequestDataRepository>();
            services.AddScoped<RequestDetailRepository>();
            services.AddScoped<RequestDocumentRepository>();
            services.AddScoped<RequestStatuRepository>();
            services.AddScoped<RequestTransactionRepository>();
            services.AddScoped<ServiceCategoryRepository>();
            services.AddScoped<ServiceDataRepository>();
            services.AddScoped<ServiceParameterRepository>();
            services.AddScoped<ServiceRequiredDocumentRepository>();
            services.AddScoped<TopUpHistoryRepository>();
            services.AddScoped<WilayatRepository>();




            return services;
        }
    }
}
