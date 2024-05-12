using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ProjectMember : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;
}
