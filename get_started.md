# Начало работы

## Запуск контекста

```csharp
var security = new Core.Security("MyNewTestApp", "MyNewTestApp Description");
//или если вы работаете со сборкой SecurityHttp, то
var security = new SecurityWebClient("MyNewTestApp", "MyNewTestApp Description");
```

Здесь происходит создание экземпляра класса, реализующего интерфейс `ISecurity` и регистрация приложения "MyNewTestApp". Далее, необходимо зарегистрировать политики безопасности

```csharp
Config.RegisterSecurityObjects("MyNewTestApp"
    , "politic1"
    , "politic2"
    , "politic3"
    , "politic4"
    , "politic5");
```

Итоговый код будет примерно таким:

```csharp
    public class WithDbSecurity: Security.Core.Security
    {
        public WithDbSecurity() : base("MvcWithDbApplication", "Тестовое приложение MvcWithDbApplication")
        {
            Config.RegisterSecurityObjects("MvcWithDbApplication", "contact");
        }
    }
```

Рекомендуется, использовать дочерний класс, наследующий контекст безопасности и использовать его через DI-контейнер

```csharp
//IoC Autofac
//AspNet MVC project

var builder = new ContainerBuilder();

builder.RegisterControllers(typeof(MvcApplication).Assembly);
builder.RegisterType<WithDbSecurity>().As<ISecurity>();

DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
```

## Токены

Принцип работы осуществляется на основе передаче токенов. 
В первый раз, при входе пользователя, в систему передаются логин и пароль для входа. 
При успешном входе приложение получает токен пользователя, которое необходимо использовать в дальнейшем
для проверок доступа.