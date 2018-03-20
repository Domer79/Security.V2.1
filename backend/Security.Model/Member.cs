using System;
using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект "Участник безопасности"
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Дополнительный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Иденификатор участника в БД
        /// </summary>
        public int IdMember { get; set; }

        /// <summary>
        /// Имя участника
        /// </summary>
        public string Name { get; set; }
    }
}
