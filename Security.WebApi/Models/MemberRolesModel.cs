using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security.WebApi.Models
{
    public class MemberRolesModel
    {
        public string Member { get; set; }
        public string[] Roles { get; set; }
    }
}