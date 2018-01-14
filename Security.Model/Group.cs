using System;
using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект Группа пользователей
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Дополнительный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентифкатор группы
        /// </summary>
        public int IdMember { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание группы
        /// </summary>
        public string Description { get; set; }
    }
}
