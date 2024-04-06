using Maritza.Controllers.Base;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.DTO;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Maritza.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        UsersB UsersB = new UsersB();
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            dtoLogin dtoLogin = new dtoLogin();
            return View(dtoLogin);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(dtoLogin login)
        {
            if (login == null)
                ViewBag.ErrorMessage = "Sucedio un error al iniciar sesion";
            try
            {
                var User = UsersB.getUserByUserName(login.UserName);
                if (User == null)
                {
                    ViewBag.ErrorMessage = "Usuario no encontrado";
                    return View();
                }

                bool IsValidPassword = User.Password == login.Password ? true : false;

                //if (IsValidPassword)
                if (true)
                {
                    DateTime Expires = DateTime.Now.AddDays(1);

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                        1,
                        User.UserID.ToString(),
                        DateTime.Now,
                        Expires,
                        User.RememberPassword,
                        login.UserName,
                        FormsAuthentication.FormsCookiePath
                        );

                    HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));
                    Cookie.Expires = Ticket.Expiration;
                    Response.Cookies.Add(Cookie);

                    HttpCookie cookieUsername = new HttpCookie("CookieUsername", login.UserName);
                    cookieUsername.Expires = Ticket.Expiration;
                    Response.Cookies.Add(cookieUsername);

                    HttpCookie cookieRol = new HttpCookie("Rol", User.RoleName);
                    cookieRol.Expires = Ticket.Expiration;
                    Response.Cookies.Add(cookieRol);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Contraseña no valida";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Sucedio un error al iniciar sesion";
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            var req = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (req != null)
            {
                var User = new HttpCookie("CookieUsername");
                User.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(User);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult Register()
        {
            dtoRegister dtoRegister = new dtoRegister();
            return View(dtoRegister);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(dtoRegister dtoRegister)
        {
            if (dtoRegister == null)
                ViewBag.ErrorMessage = "Sucedio un error al iniciar sesion";
            try
            {

                if (UsersB.CheckIfExistsEmail(dtoRegister.Email))
                {
                    return View(dtoRegister);
                }
                if (UsersB.CheckIfExistsUserName(dtoRegister.UserName))
                {
                    return View(dtoRegister);
                }

                //encrypt password

                tblUsers newUser = new tblUsers()
                {
                    Name = dtoRegister.Name,
                    UserName = dtoRegister.UserName,
                    Password = dtoRegister.Password,
                    Email = dtoRegister.Email,
                    RolID = 2,
                    Active = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                };


                Response resposne = UsersB.Create(newUser);
                if (resposne.Result != Result.Ok)
                {
                    //Error base de datos
                    return View(dtoRegister);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Sucedio un error al iniciar sesion";
            }
            return RedirectToAction("Login");
        }

    }
}
