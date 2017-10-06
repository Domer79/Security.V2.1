namespace Security.Ioc
{
    public class Container
    {
        private readonly IContainer _autofacContainer;
        private static Container _instance;

        public static void LoadContainer(Container container)
        {
            _instance = container;
        }

        public Container(params IModule[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            _autofacContainer = builder.Build();
        }

        public static T Resolve<T>() where T: class 
        {
            return _instance._autofacContainer.Resolve<T>();
        }
    }
}