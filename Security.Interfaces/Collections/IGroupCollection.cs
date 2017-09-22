using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий собой коллекцию групп пользователей
    /// </summary>
    public interface IGroupCollection : IQueryableCollection<Group>
    {
    }
}