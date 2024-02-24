using Neo4jClient;
using StarJetAirways.API.Configuration.Settings;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.ServiceContracts;
using StarJetAirways.Core.Services;
using StarJetAirways.Infrastructure.Repositories;

namespace StarJetAirways.API.Configurations.StartupExtensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region setup Neo4j
        Neo4jConnectionSettings? neo4JConnectionSettings = configuration.GetSection("Neo4j").Get<Neo4jConnectionSettings>();

        var neo4jDBClient = new BoltGraphClient(
            new Uri(neo4JConnectionSettings.Uri),
            neo4JConnectionSettings.Username,
            neo4JConnectionSettings.Password);

        neo4jDBClient.ConnectAsync().GetAwaiter().GetResult();

        services.AddSingleton<IGraphClient>(neo4jDBClient);
        #endregion

        #region add services into IoC
        //automapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //airports
        services.AddScoped<IAirportsRepository, AirportsRepository>();
        services.AddScoped<IAirportsGetterService, AirportsGetterService>();
        services.AddScoped<IAirportsAdderService, AirportsAdderService>();

        //airlines
        services.AddScoped<IAirlinesRepository, AirlinesRepository>();
        services.AddScoped<IAirlinesGetterService, AirlinesGetterService>();
        services.AddScoped<IAirlinesAdderService, AirlinesAdderService>();

        //aircrafts
        services.AddScoped<IAircraftsRepository, AircraftsRepository>();
        services.AddScoped<IAircraftsGetterService, AircraftsGetterService>();
        services.AddScoped<IAircraftsAdderService, AircraftsAdderService>();

        #endregion


        //return services
        return services;
    }
}
