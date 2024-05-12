using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ProjectTask
{
    public int Number {  get; set; }
    public string Text { get; set; } = null!;

    [Required]
    public Guid ColumnId {  get; set; }
    public TaskColumn Column { get; set; } = null!;
}
