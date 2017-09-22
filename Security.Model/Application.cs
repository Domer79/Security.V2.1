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

        /// <summary>
        /// Список ролей приложения
        /// </summary>
        public HashSet<Role> Roles { get; set; }

        /// <summary>
        /// Список объектов безопасности приложения
        /// </summary>
        public HashSet<SecObject> SecObjects { get; set; }

        /// <summary>
        /// Список типов доступа приложения
        /// </summary>
        public HashSet<AccessType> AccessTypes { get; set; }
    }
}
