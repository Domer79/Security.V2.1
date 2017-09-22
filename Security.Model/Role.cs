using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект "Роль"
    /// </summary>
    public class Role
    {
        public Role()
        {
            Grants = new HashSet<Grant>();
        }

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

        /// <summary>
        /// Список ее разрешений
        /// </summary>
        public HashSet<Grant> Grants { get; set; }

        /// <summary>
        /// Список участников, входящих в роль
        /// </summary>
        public HashSet<Member> Members { get; set; }

        /// <summary>
        /// Наименование приложения
        /// </summary>
        public Application Application { get; set; }
    }
}
