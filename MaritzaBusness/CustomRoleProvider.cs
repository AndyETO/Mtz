using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;

namespace MaritzaBusness
{
    public class CustomRoleProvider : RoleProvider
    {
        AuxMethods AuxMethods = new AuxMethods();
        public override string ApplicationName { get; set; }
        public override bool IsUserInRole(string id, string roleName)
        {

            return false;

        }

        public override string[] GetRolesForUser(string UserID)
        {
            string rol = "";
            if (HttpContext.Current.Request.Cookies["Rol"] != null)
            {
                rol = HttpContext.Current.Request.Cookies["Rol"].Value;
            }
            else
            {
                HttpCookie cookieRol = new HttpCookie("Rol", AuxMethods.GetUserRol(UserID));
                DateTime expires = DateTime.Now.AddDays(1);
                cookieRol.Expires = expires;
                HttpContext.Current.Response.Cookies.Add(cookieRol);
            }

            return rol == null ? new string[] { } : new string[] { rol };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}
