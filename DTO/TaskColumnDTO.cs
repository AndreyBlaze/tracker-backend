using System.ComponentModel.DataAnnotations;

namespace DTO;

public class TaskColumnDTO
{
    public Guid? Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Guid DashboardId { get; set; }
}
