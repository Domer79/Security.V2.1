namespace Security.Model
{
    /// <summary>
    /// Объект разрешения
    /// </summary>
    public class Grant
    {
        /// <summary>
        /// Идентификатор объекта безопасности
        /// </summary>
        public int IdSecObject { get; set; }

        public int IdRole { get; set; }
    }
}
