using Microsoft.EntityFrameworkCore;
using Shared;
using WebApp.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);


var connectionString = Environment.GetEnvironmentVariable("OLIGO_TODO_CONNECTION_STRING");
builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"] = connectionString;

builder.Services.AddDbContext<OligoToDoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OligoToDoDbContext>();
        dbContext.Database.EnsureDeleted(); // Drop the database if it exists
        dbContext.Database.Migrate();       // Apply migrations

        UserModel user1 = new()
        {
            Username = "User 1",
            PasswordHash = "Password 1",
        };
        UserModel user2 = new()
        {
            Username = "User 2",
            PasswordHash = "Password 2",
        };

        dbContext.Users.AddRange(user1, user2);

        TaskModel task1 = new()
        {
            Title = "Task 1",
            Description = "Description 1",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(1),
            UserModelId = user1.Id,
            Owner = user1
        };
        TaskModel task2 = new()
        {
            Title = "Task 2",
            Description = "Description 2",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(2),
            UserModelId = user2.Id,
            Owner = user2
        };
        TaskModel task3 = new()
        {
            Title = "Task 3",
            Description = "Description 3",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(3),
            UserModelId = user2.Id,
            Owner = user2
        };

        dbContext.Tasks.AddRange(task1, task2, task3);

        dbContext.SaveChanges();
    }
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
