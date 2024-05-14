using Domain.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Project : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public Guid? FileId { get; set; }

    [ForeignKey(nameof(FileId))]
    public AppFile? AppFile { get; set; }

    [Required]
    public Guid CreatorId { get; set; }

    [ForeignKey(nameof(CreatorId))]
    public User Creator { get; set; } = null!;

    public IEnumerable<ProjectMember>? ProjectMembers { get; set; }

    [Required]
    public ProjectType Type { get; set; }
}
