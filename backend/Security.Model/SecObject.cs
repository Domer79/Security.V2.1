using System.Collections.Generic;

namespace Security.Model
{
    /// <summary>
    /// Объект безопасности
    /// </summary>
    public class SecObject
    {
        /// <summary>
        /// Идентификатор объекта в базе данных
        /// </summary>
        public int IdSecObject { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Идентификатор приложения
        /// </summary>
        public int IdApplication { get; set; }

        /// <summary>
        /// Возвращает строку, которая представляет текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public sealed override string ToString()
        {
            return ObjectName;
        }
    }
}
