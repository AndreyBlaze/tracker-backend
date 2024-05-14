using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<AppFile> AppFiles => Set<AppFile>();
    public DbSet<Dashboard> Dashboards => Set<Dashboard>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<TaskColumn> TaskColumns => Set<TaskColumn>();
    public DbSet<ReadMessage> ReadMessages => Set<ReadMessage>();

    /// <summary>
    /// On Configuring for migration
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=tracker;";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}
