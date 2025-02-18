using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RivertyTask.API.Models;
using RivertyTask.API.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RivertyTask.API", Version = "v1" });
});

ConfigurationManager Configuration = builder.Configuration;
Configuration.AddUserSecrets<Secrets>();

builder.Services.AddScoped<ICurrencyExchangeService, CurrencyExchangeService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RivertyTask.API V1"));
    
}
else
{
    app.UseHttpsRedirection(); //TODO: Find better solution for Swaggers CORS
}

app.UseAuthorization();

app.MapControllers();

app.Run();
