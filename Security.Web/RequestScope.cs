﻿using System;
using System.Web;
using Security.V2.Contracts;
using Security.V2.Core.Ioc;
using Security.V2.Core.Ioc.Interfaces;

namespace Security.Web
{
    public class RequestScope : IScope
    {
        private readonly IRegistry _registry;

        public RequestScope(IRegistry registry)
        {
            _registry = registry;
            InitScope();
        }

        void InitScope()
        {
            HttpContext.Current.ApplicationInstance.BeginRequest += ApplicationInstance_BeginRequest;
            HttpContext.Current.ApplicationInstance.EndRequest += ApplicationInstance_EndRequest;
        }

        private void ApplicationInstance_EndRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ApplicationInstance_BeginRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public object GetObject(IRequest request, Type serviceType)
        {
            throw new NotImplementedException();
        }
    }

}
