using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DbContext, ApplicationContext>(
            (serviceProvider, optionsBuilder) =>
                optionsBuilder.UseNpgsql("Server=193.32.177.8;Port=5432;User Id=root;Password=arn~os21yp~IrER;Database=platform;"));

        return services;
    }
}
