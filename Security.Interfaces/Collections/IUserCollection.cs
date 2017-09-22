using Security.Interfaces.Base;
using System.Linq;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий собой коллекцию пользователй
    /// </summary>
    public interface IUserCollection : IQueryableCollection<User>
    {
        /// <summary>
        /// Возвращает коллекцию пользователей, при этом не учитывая полученные данные
        /// </summary>
        /// <returns></returns>
        IQueryable<User> AsNoTracking();
    }
}
