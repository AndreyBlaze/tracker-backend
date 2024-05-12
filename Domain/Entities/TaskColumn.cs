using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class TaskColumn : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Guid DashboardId { get; set; }

    public IEnumerable<Task>? Tasks { get; set;}
}
