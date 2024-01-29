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
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IAirportsGetterService, AirportsGetterService>();
        services.AddScoped<IAirportsAdderService, AirportsAdderService>();
        services.AddScoped<IAirportsRepository, AirportsRepository>();
        #endregion


        //return services
        return services;
    }
}
