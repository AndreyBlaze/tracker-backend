using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Dashboard : BaseEntity
{
    [Required]
    public Guid ProjectId { get; set; }

    public IEnumerable<TaskColumn>? TaskColumns { get; set; }
}
