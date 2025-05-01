using Moamalat;
using Moamalat.Components;
using Moamalat.Entities;
using Moamalat.Services;
using Moamalat.Shared.Services;
using Moamalat.Web.Components;
using Moamalat.Web.Services;
using Moamalat.Shared;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Encryption").GetValue<string>("key");
var Salt = builder.Configuration.GetSection("Encryption").GetValue<string>("salt");
Utilities.publicKey = key;
Utilities.SaltStr = Salt;

// Register IHttpClientFactory
builder.Services.AddHttpClient();
//builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddScoped<IPlatformHandler, PlatformHandler>();

builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddScoped<ISecureDataStoreService, SecureSessionStoreService>();
builder.Services.AddScoped<IOpenExternalUrl, OpenExternalUrl>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the Moamalat.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MainLayout).Assembly);

app.Run();
