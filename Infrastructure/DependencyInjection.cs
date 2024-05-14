using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Impl;
using Infrastructure.Persistence.Repositories.Interfaces;
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

        services
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddScoped<ISessionRepository, SessionRepository>()
            .AddScoped<IProjectsRepository, ProjectsRepository>()
            .AddScoped<IDashboardsRepository, DashboardsRepository>()
            .AddScoped<IProjectMembersRepository, ProjectMembersRepository>();

        return services;
    }
}
