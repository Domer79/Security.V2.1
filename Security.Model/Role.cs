using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект "Роль"
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор роли в БД
        /// </summary>
        public int IdRole { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public int IdApplication { get; set; }
    }
}
