using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ReadMessage : BaseEntity
{
    /// <summary>
    /// Кто прочитал сообщение
    /// </summary>
    [Required]
    public Guid UserId { get; set; }
    /// <summary>
    /// id сообщения
    /// </summary>
    [Required]
    public Guid MessageId { get; set; }
    /// <summary>
    /// id комнаты 
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }
}
