using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWithDbApplication.Models;
using Security.Contracts;
using Security.Exceptions;
using Security.Web.Extensions;

namespace MvcWithDbApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISecurity _security;

        public AuthController(ISecurity security)
        {
            _security = security;
        }

        // GET: Auth
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(LogOnViewModel viewModel, string returnUrl)
        {
            try
            {
                _security.LoginAndSetCookie(viewModel.LoginOrEmail, viewModel.Password, viewModel.RememberMe);
                return Redirect(returnUrl);
            }
            catch (InvalidLoginPasswordException e)
            {
                viewModel.ErrorMessage = e.Message;
                return View(viewModel);
            }
        }
    }
}