using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Infrastructure;

static class Configuration
{
    static public string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceBackend.API"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("Redis")!;
        }
    }
}
