using MaritzaBusness;
using MaritzaData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Maritza.Controllers
{
    [Authorize]
    public class AccountController : Controller
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
            //try for errors login
            //search user
            //is valid the password

            try
            {
                var User = UsersB.getUserByUserName(login.UserName);
                if (User == null)
                {
                    ViewBag.ErrorMessage = "Usuario no encontrado";
                    return View();
                }


                bool IsValidPassword = User.Password == login.Password ? true : false;

                if (IsValidPassword)
                {
                    DateTime Expires = DateTime.Now.AddDays(1);

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                        1,
                        login.UserName,
                        DateTime.Now,
                        Expires,
                        User.RememberPassword,
                        login.UserName,
                        FormsAuthentication.FormsCookiePath
                        );
                    //coockie base
                    HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));
                    Cookie.Expires = Ticket.Expiration;
                    Response.Cookies.Add(Cookie);

                    HttpCookie cookieRol = new HttpCookie("Rol", User.RoleName);
                    cookieRol.Expires = Ticket.Expiration;
                    Response.Cookies.Add(cookieRol);
                    //Other coockies

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


    }
}
