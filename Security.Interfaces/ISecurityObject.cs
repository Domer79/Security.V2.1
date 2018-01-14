namespace Security.Interfaces
{
    /// <summary>
    /// Интерфейс объекта безопасности
    /// </summary>
    public interface ISecurityObject
    {
        /// <summary>
        /// Наименование объекта
        /// </summary>
        string ObjectName { get; set; }
    }
}