using System;

namespace Security.Model
{
    /// <summary>
    /// Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Значение токена
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Дата истекания периода действия токена
        /// </summary>
        public DateTime Expire { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому был выдан токен
        /// </summary>
        public int IdUser { get; set; }
    }
}