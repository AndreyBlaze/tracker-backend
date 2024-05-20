using Domain.Types;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ProjectMember : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public ProjectRoleType Role { get; set; }

    //[JsonIgnore]
    //public Project Project { get; set; } = null!;
}
