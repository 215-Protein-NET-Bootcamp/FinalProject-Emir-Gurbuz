using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTech.DataAccess.Contexts;

namespace NTech.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration.GetSection("Database").Value.ToLower())
            {
                case "postgresql":
                    services.AddDbContext<NTechDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
                    break;
                case "sqlserver":
                    services.AddDbContext<NTechDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
                    break;
                default:
                    services.AddDbContext<NTechDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
                    break;
            }
            builder.Services.AddScoped<DbContext, NTechDbContext>();
            return services;
        }
    }
}
