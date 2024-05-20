using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class ProjectTask : BaseEntity
{
    public int Number {  get; set; }
    public string Text { get; set; } = null!;
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public Guid ColumnId {  get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(ColumnId))]
    public TaskColumn Column { get; set; } = null!;
}
