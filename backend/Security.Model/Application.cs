using System.Collections.Generic;

namespace Security.Model
{
    public class Application
    {
        /// <summary>
        /// Идентификатор приложения
        /// </summary>
        public int IdApplication { get; set; }

        /// <summary>
        /// Имя приложения. Является первичным ключов в БД
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
