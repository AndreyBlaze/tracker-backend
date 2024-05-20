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
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=tracker;"));

        services
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddScoped<ISessionRepository, SessionRepository>()
            .AddScoped<IProjectsRepository, ProjectsRepository>()
            .AddScoped<IDashboardsRepository, DashboardsRepository>()
            .AddScoped<IProjectMembersRepository, ProjectMembersRepository>()
            .AddScoped<ITaskColumnsRepository, TaskColumnsRepository>()
            .AddScoped<ITasksRepository, TasksRepository>();

        return services;
    }
}
