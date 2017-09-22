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
        /// Идентификатор типа доступа
        /// </summary>
        public int IdAccessType { get; set; }

        /// <summary>
        /// Список разрешений, в котором задействован данный объект
        /// </summary>
        public HashSet<Grant> Grants { get; set; }

        /// <summary>
        /// Приложения
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// Тип доступа
        /// </summary>
        public AccessType AccessType { get; set; }

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
