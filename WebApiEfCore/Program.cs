using Bogus;
using Microsoft.EntityFrameworkCore;
using WebApiEfCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                ));



WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string[] summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/generate-users", async (DBContext dbcontext) =>
{

    var faker = new Faker<User>()
    .UseSeed(65656)
    .RuleFor(u=> u.Age, (f,u) => f.Random.Int(5,60))
    .RuleFor(u=> u.Name, (f,u) => f.Person.FirstName)
    .RuleFor(u=> u.Email, (f,u) => f.Person.Email)
    .RuleFor(u=> u.Password, (f,u) => f.Internet.Password())
    .RuleFor(u=> u.Phone, (f,u) => f.Phone.ToString() )
    .RuleFor(u=> u.PhoneNumber, (f,u) => f.Phone.ToString() )
    .RuleFor(u=> u.PhoneNumberConfirmed, (f,u) => f.Random.Bool().ToString() )
    ;

    var users = faker.Generate(100);

    await dbcontext.Users.AddRangeAsync(users);
    await dbcontext.SaveChangesAsync();

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
