using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Core.Ioc.Interfaces;
using Security.V2.Core.Ioc.Scopes;

namespace Security.V2.Core.Ioc
{
    public static class Service
    {
        public static void InSingletonScope(this IDependency dependency)
        {
            if (dependency.Scope != null)
                dependency.Scope.Dispose();

            dependency.Scope = new SingletonScope(dependency.Registry);
        }
    }
}
