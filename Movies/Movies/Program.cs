using Microsoft.EntityFrameworkCore;
using Movies.Endpoints;
using Movies.Persistance;
using Movies.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MovieDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

builder.Services.AddTransient<IMovieService, MovieService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMovieEndpoints();

app.Run();
