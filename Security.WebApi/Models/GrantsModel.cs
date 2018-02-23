using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security.WebApi.Models
{
    public class GrantsModel
    {
        public string Role { get; set; }
        public string[] SecObjects { get; set; }
    }
}