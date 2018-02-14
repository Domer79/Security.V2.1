using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Security.Model;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity
{
    public class MapperMappings
    {
        public static void Map()
        {
            Mapper.Initialize(cfg =>
            {
                #region POCO to ViewModel

                cfg.CreateMap<User, UserViewModel>();

                #endregion

                #region ViewModel to POCO

                cfg.CreateMap<UserViewModel, User>();

                #endregion

                cfg.CreateMap<UserViewModel, UserViewModel>();
            });
        }
    }
}
