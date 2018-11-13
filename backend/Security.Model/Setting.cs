using System;

namespace Security.Model
{
    /// <summary>
    /// Объект настройки для системы доступа
    /// </summary>

    public class Setting
    {      
        /// <summary>
        /// Идентификатор в БД
        /// </summary>
        public int IdSettings { get; set; }

        /// <summary>
        /// Ключ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата изменения значения
        /// </summary>
        public DateTime ChangedDate { get; set; }

        /// <summary>
        /// Время жизни (сохраняется в БД)
        /// </summary>
        public long? InDbLifetime { get; set; }

        /// <summary>
        /// Время жизни
        /// </summary>
        public TimeSpan Lifetime
        {
            get => TimeSpan.FromTicks(InDbLifetime.HasValue ? InDbLifetime.Value : long.MaxValue);
            set => InDbLifetime = value.Ticks;
        }

        /// <summary>
        /// Значение актуально/устарело
        /// </summary>
        public bool Deprecated => DateTime.Now > ChangedDate.Add(Lifetime);
    }
}