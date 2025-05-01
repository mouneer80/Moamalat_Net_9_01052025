using Microsoft.EntityFrameworkCore;
using Moamalat.Api;
using Moamalat.Data;
using Moamalat.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


var key = builder.Configuration.GetSection("Encryption").GetValue<string>("key");
var Salt = builder.Configuration.GetSection("Encryption").GetValue<string>("salt");
Utilities.publicKey = key;
Utilities.SaltStr = Salt;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextFactory<MoamalatDb_Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();
builder.Services.AddHttpClient<HttpClient>(
    "PGHttpClient",
    client => client.BaseAddress = new Uri(builder.Configuration.GetSection("Payment").GetValue<string>("ApiUrl"))
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Moamalat API", Version = "v1" });
});


builder.Services.AddOpenApi();

builder.Services.AddAuthentication()
                .AddJwtBearer();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization() ;

app.MapControllers();

app.Run();
