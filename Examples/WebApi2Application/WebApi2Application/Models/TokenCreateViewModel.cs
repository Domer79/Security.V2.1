using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2Application.Models
{
    public class TokenCreateViewModel
    {
        public string LoginOrEmail { get; set; }
        public string Password { get; set; }
    }
}