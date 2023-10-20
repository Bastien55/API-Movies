using API_Movies.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API_Movies;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        // Replace with your connection string.
        var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Replace with your server version and type.
        // Use 'MariaDbServerVersion' for MariaDB.
        // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
        // For common usages, see pull request #1233.
        var serverVersion = new MariaDbServerVersion(new Version(11, 1, 2));

        // Replace 'YourDbContext' with the name of your own DbContext derived class.
        services.AddDbContext<ApiMovieContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(config.GetConnectionString("MoviesDatabase"), serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
    }
}