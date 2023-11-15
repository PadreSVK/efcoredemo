using WebApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<BetterWorker>();

//appsettings.json
//appsettings.{ASPNETCORE_ENVIRONMENT.Value}.json
//appsettings.Development.json
//appsettings.Production.json

// env variables
// user secrets
//cmd args


var aconfig = builder.Configuration.GetDebugView();

Console.WriteLine(builder.Configuration["ASPNETCORE_ENVIRONMENT"]);
Console.WriteLine(builder.Configuration["test-args"]);


if (builder.Configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
{
    builder.Services.AddSingleton<ILogService, NewLogService>();
    //factoryILogService
    builder.Services.AddSingleton<Func<ILogService>>(sp => () =>  sp.GetRequiredService<ILogService>());
}
else
{
    builder.Services.AddSingleton<ILogService, MyService>();
}

builder.Services.AddSingleton<MyConfiguration>(sp => new MyConfiguration
{
    Level = "asdsa",
    MyProperty = 41
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger(); //generate json
    _ = app.UseSwaggerUI(); //ui
}

app.UseMiddleware<MyMiddleware>();

app.UseHttpsRedirection();

string[] summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    WeatherForecast[] forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
