using System;
using System.Threading.Tasks;
using NLog;
using Security.Contracts;

namespace Security.Tests.Scenarios
{
    public abstract class BaseScenario<TScenarioResult> : IDisposable
    {
        private readonly Logger _logger;
        private ISecurity _security;
        private bool _isAsync = false;

        protected BaseScenario()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <returns></returns>
        public Task<TScenarioResult> RunAsync(ISecurity security)
        {
            return RunAsync(security, null);
        }

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <param name="someTask">Действие, которое нужно совершить за пределами сценария</param>
        /// <returns></returns>
        public async Task<TScenarioResult> RunAsync(ISecurity security, Func<object, Task<object>> someTask)
        {
            _isAsync = true;
            try
            {
                _security = security;
                return await _RunAsync(security, someTask);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Выполняет откат сценария
        /// Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync(ISecurity security)
        {
            if (!_isAsync)
                return;

            try
            {
                await _RollbackAsync(security);
            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    _logger.Error(exception, "AggregateException");
                    Console.WriteLine(exception);
                }

                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <param name="someTask">Действие, которое нужно совершить за пределами сценария</param>
        /// <returns></returns>
        protected abstract Task<TScenarioResult> _RunAsync(ISecurity security, Func<object, Task<object>> someTask);

        /// <summary>
        /// Выполняет откат сценария
        /// Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста
        /// </summary>
        /// <returns></returns>
        protected abstract Task _RollbackAsync(ISecurity security);

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <returns></returns>
        public TScenarioResult Run(ISecurity security)
        {
            return Run(security, null);
        }

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <param name="someTask">Действие, которое нужно совершить за пределами сценария</param>
        /// <returns></returns>
        public TScenarioResult Run(ISecurity security, Func<object, Task<object>> someTask)
        {
            try
            {
                _security = security;
                return _Run(security, someTask);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Выполняет откат сценария
        /// Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста
        /// </summary>
        /// <returns></returns>
        public void Rollback(ISecurity security)
        {
            if (_isAsync)
                return;

            try
            {
                _Rollback(security);
            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    _logger.Error(exception, "AggregateException");
                    Console.WriteLine(exception);
                }

                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Выполняет сценарий. 
        /// </summary>
        /// <param name="security">Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста</param>
        /// <param name="someTask">Действие, которое нужно совершить за пределами сценария</param>
        /// <returns></returns>
        protected abstract TScenarioResult _Run(ISecurity security, Func<object, Task<object>> someTask);

        /// <summary>
        /// Выполняет откат сценария
        /// Если контекст указан, работа с БД с должна осуществляться в пределах указанного контекста
        /// </summary>
        /// <returns></returns>
        protected abstract void _Rollback(ISecurity security);

        public void Dispose()
        {
            if (_security == null)
                return;

            Rollback(_security);
            RollbackAsync(_security).Wait();
        }
    }
}
