using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AppFile : BaseEntity
{
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
