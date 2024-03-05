using Neo4jClient;
using StarJetAirways.API.Configuration.Settings;
using StarJetAirways.Core.Domain.RepositoryContracts;
using StarJetAirways.Core.ServiceContracts;
using StarJetAirways.Core.Services;
using StarJetAirways.Core.Services.SearchFlights;
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
        services.AddScoped<IAirportsCheckerService, AirportsCheckerService>();

        //airlines
        services.AddScoped<IAirlinesRepository, AirlinesRepository>();
        services.AddScoped<IAirlinesGetterService, AirlinesGetterService>();
        services.AddScoped<IAirlinesAdderService, AirlinesAdderService>();
        services.AddScoped<IAirlinesCheckerService, AirlinesCheckerService>();

        //aircrafts
        services.AddScoped<IAircraftsRepository, AircraftsRepository>();
        services.AddScoped<IAircraftsGetterService, AircraftsGetterService>();
        services.AddScoped<IAircraftsAdderService, AircraftsAdderService>();
        services.AddScoped<IAircraftsCheckerService, AircraftsCheckerService>();

        //flights
        services.AddScoped<IFlightsRepository, FlightsRepository>();
        services.AddScoped<IFlightsGetterService, FlightsGetterService>();
        services.AddScoped<IFlightsAdderService, FlightsAdderService>();
        services.AddScoped<IFlightsCheckerService, FlightsCheckerService>();

        services.AddScoped<FlightsSearch, GeneralFlightsSearch>();

        #endregion


        //return services
        return services;
    }
}
