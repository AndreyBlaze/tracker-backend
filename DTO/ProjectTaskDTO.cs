using System.ComponentModel.DataAnnotations;

namespace DTO;

public class ProjectTaskDTO
{
    public Guid? Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public Guid ColumnId { get; set; }
    [Required]
    public Guid ProjectId { get; set; }

    public Guid? UserId { get; set; }
}
