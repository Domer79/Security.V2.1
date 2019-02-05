# Security.Web

Этот модуль явялется расширением системы безопасности для web-приложений.

## Security.Web.SecurityAuthenticateHttpModule

Это модуль, реализующий интерфейс IHttpModule и который проверяет аутентифицирован пользователь или нет и если да, 
свои экземляры классов `IPrincipal` и `IIdentity`

### Подключение модуля

Подключение модуля производится в файле конфигурации

```xml
  <system.webServer>
    <modules>
      <remove name="SecurityAuthenticateHttpModule"/>
      <add name="SecurityAuthenticateHttpModule" type="Security.Web.SecurityAuthenticateHttpModule, Security.Web"/>
    </modules>
  </system.webServer>
```

## Security.Web.UserPrincipal

Класс, реализующий интерфейс IPrincipal

## Security.Web.UserIdentity

Класс, реализующий интерфейс IIdentity, в свойстве Name, которого хранится токен пользователя. 
Для получения подробной информации по пользователю, можно обратиться к методам расширения 
`GetLogin()` и `GetUser()`, расположенными в классе `Security.Web.Extensions.Identity`

## Security.Web.Http.AuthorizeAttribute

Атрибут, который предназначен для проверки доступа в приложениях ASP.NET WebApi

## Security.Web.Mvc.AuthorizeAttribute

Атрибут, который предназначен для проверки доступа в приложениях ASP.NET MVC