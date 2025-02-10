using WorkerService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<ExchangeRateService>();
builder.Services.AddHostedService<TimedHostedService>();

var host = builder.Build();
host.Run();
