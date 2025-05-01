using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moamalat.Entities;
using Moamalat.Services;
using Moamalat.Shared;
using Moamalat.Shared.Services;
using System.Buffers.Text;
using System.Reflection;

namespace Moamalat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var res = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var appSettingsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Moamalat.appsettings.json");
            var config = new ConfigurationBuilder()
            .AddJsonStream(appSettingsStream)
            .Build();

            var key = config.GetSection("Encryption").GetValue<string>("key");
            var Salt = config.GetSection("Encryption").GetValue<string>("salt");
            Utilities.publicKey = key;
            Utilities.SaltStr = Salt;


            builder.Configuration.AddConfiguration(config);

            // Add device-specific services used by the Moamalat.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
           
            //Utilities.publicKey = "SALG2AZALY69572";
            //Utilities.SaltStr = "27596YLAZA2GLAS";


            builder.Services.AddServices(builder.Configuration);

            builder.Services.AddScoped<ISecureDataStoreService, SecureSessionStoreService>();
            builder.Services.AddScoped<IOpenExternalUrl, OpenExternalUrl>();

            // builder.Services.AddScoped<IPickFileControl, PickFileControl>();

            //builder.Services.AddScoped<ISigninManager, SigninManager>();
            builder.Services.AddSingleton<IPlatformHandler, PlatformHandler>();



            //string BaseUrl = "https://api.moamalt.com/";

            //builder.Services.AddHttpClient<HttpClient>(
            //    "BaseHttpClient",
            //    client => client.BaseAddress = new Uri(BaseUrl)
            //    ).AddHttpMessageHandler<HttpRequestHandler>();


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
