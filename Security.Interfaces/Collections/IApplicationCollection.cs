using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий коллекцию приложений
    /// </summary>
    public interface IApplicationCollection : IQueryableCollection<Application>
    {
    }
}
