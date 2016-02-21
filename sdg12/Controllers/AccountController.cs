using Microsoft.Owin;
using MediatR;
using Microsoft.AspNet.Identity;
using sdg12.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;

namespace sdg12.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: Account
        public virtual ActionResult Login(string username, string password)
        {
            var loginCommand = new LoginCommand
            {
                Username = username,
                Password = password
            };
            var result = mediator.Send(loginCommand);

            if (result.IsValidUser)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, result.UserName));
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public virtual ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}