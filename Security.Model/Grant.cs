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

        public int IdAccessType { get; set; }

        /// <summary>
        /// Объект безопасности
        /// </summary>
        public SecObject SecObject { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Тип доступа
        /// </summary>
        public AccessType AccessType { get; set; }
    }
}
