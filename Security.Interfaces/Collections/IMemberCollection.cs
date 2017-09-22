using System.Linq;
using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий собой коллекцию участников безопасности
    /// </summary>
    public interface IMemberCollection : ISecurityQueryable<Member>
    {
        /// <summary>
        /// Возвращает участников безопасности с информацией об их ролях
        /// </summary>
        /// <returns></returns>
        IQueryable<Member> WithRoles();
    }
}