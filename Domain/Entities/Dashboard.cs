using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Dashboard : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public Guid ProjectId { get; set; }

    public IEnumerable<TaskColumn>? TaskColumns { get; set; }
}
