using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий собой коллекцию ролей
    /// </summary>
    public interface IRoleCollection : IQueryableCollection<Role>
    {
        
    }
}