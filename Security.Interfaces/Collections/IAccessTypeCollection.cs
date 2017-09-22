using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий коллекцию типов доступа
    /// </summary>
    public interface IAccessTypeCollection : IQueryableCollection<AccessType>
    {
    }
}