
using ParcelTrackingAPI.Configurations;
using ParcelTrackingAPI.Model;
using ParcelTrackingAPI.Repository;

namespace ParcelTrackingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ParcelMongoDB>(
                builder.Configuration.GetSection("MongoDbParcelSettings"));

            builder.Services.AddSingleton<ILocationRepository, LocationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapGet("/test", () => "Server is running");

            app.MapGet("/track/", async (string parcelID, ILocationRepository locationRepos) =>
            {
                return await locationRepos.Get(parcelID);
            });

            app.MapPost("/create/", async (Location location, ILocationRepository locationRepos) =>
            {
                await locationRepos.Add(location);
            });

            app.MapPut("/update/", async (Location location, ILocationRepository locationRepos) =>
            {
                await locationRepos.Update(location);
            });

            app.Run();
        }
    }
}
