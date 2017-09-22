using System;

namespace Security.Model
{
    /// <summary>
    /// Запись в журнале
    /// </summary>
    public class Log
    {
        public Log()
        {
            DateCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Идентификатор лога
        /// </summary>
        public int IdLog { get; set; }

        /// <summary>
        /// Сообщение для сохранения в лог
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дата возникновения записи
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
