using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Security.Model;
using WpfApp_TestSecurity.AbonentBL.Models;
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
                cfg.CreateMap<Role, RoleViewModel>();
                cfg.CreateMap<SecObject, PolicyViewModel>().ForMember(p => p.Name, e => e.MapFrom(s => s.ObjectName));
                cfg.CreateMap<Abonent, AbonentViewModel>();

                #endregion

                #region ViewModel to POCO

                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<RoleViewModel, Role>();
                cfg.CreateMap<PolicyViewModel, SecObject>().ForMember(s => s.ObjectName, e => e.MapFrom(s => s.Name));
                cfg.CreateMap<AbonentViewModel, Abonent>();

                #endregion

                cfg.CreateMap<UserViewModel, UserViewModel>();
                cfg.CreateMap<RoleViewModel, RoleViewModel>();
                cfg.CreateMap<PolicyViewModel, PolicyViewModel>();
            });
        }
    }
}
