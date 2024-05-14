using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProjectTask : BaseEntity
{
    public int Number {  get; set; }
    public string Text { get; set; } = null!;

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public Guid ColumnId {  get; set; }
    [ForeignKey(nameof(ColumnId))]
    public TaskColumn Column { get; set; } = null!;
}
