using Security.Model;

namespace Security.Contracts
{
    /// <summary>
    /// Контекст приложения
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Приложение
        /// </summary>
        Application Application { get; }
    }
}
