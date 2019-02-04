# ������ ������

## ������ ���������

```csharp
var security = new Core.Security("MyNewTestApp", "MyNewTestApp Description");
//��� ���� �� ��������� �� ������� SecurityHttp, ��
var security = new SecurityWebClient("MyNewTestApp", "MyNewTestApp Description");
```

����� ���������� �������� ���������� ������, ������������ ��������� `ISecurity` � ����������� ���������� "MyNewTestApp". �����, ���������� ���������������� �������� ������������

```csharp
Config.RegisterSecurityObjects("MyNewTestApp"
    , "politic1"
    , "politic2"
    , "politic3"
    , "politic4"
    , "politic5");
```

�������� ��� ����� �������� �����:

```csharp
    public class WithDbSecurity: Security.Core.Security
    {
        public WithDbSecurity() : base("MvcWithDbApplication", "�������� ���������� MvcWithDbApplication")
        {
            Config.RegisterSecurityObjects("MvcWithDbApplication", "contact");
        }
    }
```

�������������, ������������ �������� �����, ����������� �������� ������������ � ������������ ��� ����� DI-���������

```csharp
//IoC Autofac
//AspNet MVC project

var builder = new ContainerBuilder();

builder.RegisterControllers(typeof(MvcApplication).Assembly);
builder.RegisterType<WithDbSecurity>().As<ISecurity>();

DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
```

## ������

������� ������ �������������� �� ������ �������� �������. 
� ������ ���, ��� ����� ������������, � ������� ���������� ����� � ������ ��� �����. 
��� �������� ����� ���������� �������� ����� ������������, ������� ���������� ������������ � ����������
��� �������� �������.