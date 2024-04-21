using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AppFile
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Кто загрузил файл
    /// </summary>
    public Guid? UserId { get; set; }
    /// <summary>
    /// Пользователь
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
    /// <summary>
    /// Название файла
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// Размер файла
    /// </summary>
    public long FileSize { get; set; } = 0;
    /// <summary>
    /// Дата загрузки
    /// </summary>
    public DateTimeOffset DateAdd { get; set; } = DateTimeOffset.UtcNow;
    /// <summary>
    /// Удален ли файл
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    #region NotMapped fields
    /// <summary>
    /// Базовый урл
    /// </summary>
    [NotMapped]
    readonly string BaseUrlAppFile = "https://api.messenger.mosritual.ru/upload/";
    /// <summary>
    /// Полный путь к файлу
    /// </summary>
    [NotMapped]
    public string FullUrl
    {
        get
        {
            return BaseUrlAppFile + FileName;
        }
    }
    #endregion
}
