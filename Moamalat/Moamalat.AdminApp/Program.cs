using Moamalat;
using Moamalat.Components;
using Moamalat.Entities;
using Moamalat.Services;
using Moamalat.Shared.Services;
using Moamalat.AdminApp.Components;
using Moamalat.AdminApp.Services;
using Moamalat.Shared;
using Moamalat.AdminApp;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Encryption").GetValue<string>("key");
var Salt = builder.Configuration.GetSection("Encryption").GetValue<string>("salt");
Utilities.publicKey = key;
Utilities.SaltStr = Salt;

builder.Services.AddHttpClient();
 
builder.Services.AddServices(builder.Configuration);

builder.Services.AddScoped<IPlatformHandler, PlatformHandler>();

builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddScoped<ISecureDataStoreService, SecureSessionStoreService>();
builder.Services.AddScoped<IOpenExternalUrl, OpenExternalUrl>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IFormFactor, FormFactor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

if (!File.Exists(Path.Combine(app.Environment.WebRootPath, "_content", "Moamalat.Shared")))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "..", "Moamalat.Shared", "wwwroot")),
        RequestPath = "/_content/Moamalat.Shared"
    });
}

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(
        typeof(MainLayout).Assembly);

app.Run();
