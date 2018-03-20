using System;
using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект "Пользователь"
    /// </summary>
    public class User
    {
        /// <summary>
        /// Дополнительный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор в БД
        /// </summary>
        public int IdMember { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get => Name;
            set => Name = value;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Статус - Активен/Заблокироан
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Строка данных, которая передаётся хеш-функции вместе с паролем
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Дата последней активности пользователя
        /// </summary>
        public DateTime? LastActivityDate { get; set; }
    }
}
