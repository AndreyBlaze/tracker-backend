using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Room : BaseEntity
{
    /// <summary>
    /// Создатель комнаты
    /// </summary>
    public string? CreatorId { get; set; }
    /// <summary>
    /// Name for room
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Тип чата
    /// </summary>
    public RoomType Type { get; set; }
    /// <summary>
    /// Аватарка
    /// </summary>
    public string? Avatar { get; set; }
    /// <summary>
    /// ID аватарки
    /// </summary>
    public Guid? FileId { get; set; }
    /// <summary>
    /// Аватарка (картинка)
    /// </summary>
    [ForeignKey("FileId")]
    public virtual AppFile? AppFile { get; set; }
    /// <summary>
    /// Участники комнаты
    /// </summary>
    public virtual IEnumerable<ProjectMember>? RoomUsers { get; set; }
    /// <summary>
    /// Сообщения
    /// </summary>
    public virtual IEnumerable<Message>? Messages { get; set; }
}
