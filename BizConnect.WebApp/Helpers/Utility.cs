using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BizConnect.Data;
using BizConnect.Data.Context;
using BizConnect.Data.Entities;
using BizConnect.WebApp.Controllers;
using BizConnect.WebApp.Models;

namespace BizConnect.WebApp.Helpers
{
    public static class Utility
    {

        private static BizConnectContext ctx = new BizConnectContext();

        public static bool Register(RegisterModel model, UserType userType, Company company, bool login)
        {
            //if (ModelState.IsValid)
            //{
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var membershipUser = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    if (userType == UserType.Owner)
                        Roles.AddUserToRoles(model.UserName, new[] { "Owner", "Admin" });
                    else if (userType == UserType.Admin)
                        Roles.AddUserToRole(model.UserName, "Admin");

                    var user = ctx.Users.Find(membershipUser.ProviderUserKey);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    ctx.Employees.Add(new Employee {Account = user, Companies = new Collection<Company>{company}});
                    if (!ctx.GetValidationErrors().Any())
                    {
                        ctx.SaveChanges();
                        if (login) FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                        return true;
                        //return RedirectToAction("Index", "Home");

                    }
                    else
                        Membership.DeleteUser(model.UserName);
                }

                //ModelState.AddModelError("", ErrorCodeToString(createStatus));
            //}

            // If we got this far, something failed, redisplay form
            //return View(model);
            return false;

        }
    }
}