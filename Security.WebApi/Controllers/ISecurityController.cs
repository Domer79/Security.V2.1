using System.Threading.Tasks;
using System.Web.Http;

namespace Security.WebApi.Controllers
{
    public interface ISecurityController<T>
    {
        /// <summary>
        /// Возвращает список всех объектов приложения
        /// </summary>
        /// <returns></returns>
        Task<IHttpActionResult> Get();

        /// <summary>
        /// Возвращает объект приложения по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IHttpActionResult> Get(int id);

        /// <summary>
        /// Создает новый объект
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IHttpActionResult> Post([FromBody] T entity);

        /// <summary>
        /// Обновляет имя или описание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns
        Task<IHttpActionResult> Put([FromBody] T entity);

        /// <summary>
        /// Удаляет объект по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IHttpActionResult> Delete(int id);

        /// <summary>
        /// Создает новый объект в базе данных со сначениями по умолчанию
        /// </summary>
        /// <param name="prefix">Префикс для значений по умолчанию</param>
        /// <returns></returns>
        Task<IHttpActionResult> CreateEmpty(string prefix);

        /// <summary>
        /// Возвращает объект приложения по его имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IHttpActionResult> GetByName(string name);
    }
}