using Domain.Types;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Message : BaseEntity
{
    public Guid? UserId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
    /// <summary>
    /// В какую комнату отправил
    /// </summary>
    public Guid? ProjectId { get; set; }
    /// <summary>
    /// Room object
    /// </summary>
    [ForeignKey(nameof(ProjectId))]
    public virtual Project? Project { get; set; }
    /// <summary>
    /// Тип сообщения
    /// 1 - Text
    /// 2 - File
    /// 3 - Emoji 
    /// 4 - Contact
    /// 5 - Call
    /// </summary>
    public MessageType Type { get; set; } = MessageType.Text;
    /// <summary>
    /// Прикрепленный файл
    /// </summary>
    public Guid? FileId { get; set; }
    /// <summary>
    /// File
    /// </summary>
    [ForeignKey("FileId")]
    public virtual AppFile? AppFile { get; set; }
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string? Body { get; set; }
    /// <summary>
    /// Список тех кто прочитал сообщение
    /// </summary>
    [JsonIgnore]
    public virtual List<ReadMessage>? ReadMessages { get; set; }
    /// <summary>
    /// ID пересланного сообщения
    /// </summary>
    public Guid? ForwardedId { get; set; }
    /// <summary>
    /// Пересланное сообщение
    /// </summary>
    [ForeignKey("ForwardedId")]
    public Message? Forwarded { get; set; }
}
