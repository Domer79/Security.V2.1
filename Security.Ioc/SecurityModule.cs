using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Registration;
using Security.BusinessLogic;
using Security.Interfaces;
using Security.Interfaces.V2;

namespace Security.Ioc
{
    public class SecurityModule: Module
    {
        public static ISecurity Create(string appName)
        {
            return new CoreSecurity(appName);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SecurityContext>().As<ISecurityContext>().SingleInstance();
            builder.Register(c => new AnonymousUser()
            {
                Login = AnonymousUser.Token
            }).AsSelf();
        }
    }
}
