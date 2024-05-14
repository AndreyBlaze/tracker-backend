using System.ComponentModel.DataAnnotations;
using Domain.Types;

namespace DTO;

public class ProjectDTO
{
    public Guid? Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;

    public Guid? FileId { get; set; }

    [Required]
    public ProjectType Type { get; set; }
}
