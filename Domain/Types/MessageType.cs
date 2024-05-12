namespace Domain.Types;

public enum MessageType
{
    /// <summary>
    /// System message
    /// </summary>
    System = -1,
    /// <summary>
    /// Text
    /// </summary>
    Text = 1,
    /// <summary>
    /// File
    /// </summary>
    File = 2,
    /// <summary>
    /// Emoji
    /// </summary>
    Emoji = 3,
    /// <summary>
    /// Contact
    /// </summary>
    Contact = 4,
    /// <summary>
    /// Call
    /// </summary>
    Call = 5,
}
