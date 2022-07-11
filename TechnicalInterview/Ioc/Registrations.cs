using TechnicalInterview.Data.Context;
using TechnicalInterview.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace TechnicalInterview.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class Registrations
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeliveryContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "DeliveryDatabase"));

            services.AddScoped<IDeliveryService, DeliveryService>();

            
        }
    }
}
