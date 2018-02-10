﻿using System;
using Security.Model;
using Security.V2.Contracts;
using ICommonDb = Security.V2.CommonContracts.ICommonDb;

namespace Security.V2.Core
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly ICommonDb _commonDb;
        private readonly string _appName;

        public ApplicationContext(ICommonDb commonDb, string appName)
        {
            _commonDb = commonDb;
            _appName = appName;
            Application = GetApplication();
        }

        private Application GetApplication()
        {
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where appName = @appName", new {appName = _appName});
        }

        public Application Application { get; }
    }
}
