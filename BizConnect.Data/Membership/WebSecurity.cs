using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BizConnect.Data.Context;
using BizConnect.Data.Entities;

namespace BizConnect.Data.Membership
{
    public sealed class WebSecurity
    {
        public static HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        public static HttpRequestBase Request
        {
            get { return Context.Request; }
        }

        public static HttpResponseBase Response
        {
            get { return Context.Response; }
        }

        public static System.Security.Principal.IPrincipal User
        {
            get { return Context.User; }
        }

        public static bool IsAuthenticated
        {
            get { return User.Identity.IsAuthenticated; }
        }

        public static MembershipCreateStatus Register(string Username, string Password, string Email, bool IsApproved, string FirstName, string LastName)
        {
            MembershipCreateStatus CreateStatus;
            System.Web.Security.Membership.CreateUser(Username, Password, Email, null, null, IsApproved, null, out CreateStatus);

            if (CreateStatus == MembershipCreateStatus.Success)
            {
                using (BizConnectContext Context = new BizConnectContext())
                {
                    User User = Context.Users.FirstOrDefault(Usr => Usr.Username == Username);
                    User.FirstName = FirstName;
                    User.LastName = LastName;
                    Context.SaveChanges();
                }

                if (IsApproved)
                {
                    FormsAuthentication.SetAuthCookie(Username, false);
                }
            }

            return CreateStatus;
        }

        public enum MembershipLoginStatus
        {
            Success, Failure
        }

        public static MembershipLoginStatus Login(string Username, string Password, bool RememberMe)
        {
            if (System.Web.Security.Membership.ValidateUser(Username, Password))
            {
                FormsAuthentication.SetAuthCookie(Username, RememberMe);
                return MembershipLoginStatus.Success;
            }
            else
            {
                return MembershipLoginStatus.Failure;
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static MembershipUser GetUser(string Username)
        {
            return System.Web.Security.Membership.GetUser(Username);
        }

        public static bool ChangePassword(string OldPassword, string NewPassword)
        {
            MembershipUser CurrentUser = System.Web.Security.Membership.GetUser(User.Identity.Name);
            return CurrentUser.ChangePassword(OldPassword, NewPassword);
        }

        public static bool DeleteUser(string Username)
        {
            return System.Web.Security.Membership.DeleteUser(Username);
        }

        public static List<MembershipUser> FindUsersByEmail(string Email, int PageIndex, int PageSize)
        {
            int totalRecords;
            return System.Web.Security.Membership.FindUsersByEmail(Email, PageIndex, PageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }

        public static List<MembershipUser> FindUsersByName(string Username, int PageIndex, int PageSize)
        {
            int totalRecords;
            return System.Web.Security.Membership.FindUsersByName(Username, PageIndex, PageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }

        public static List<MembershipUser> GetAllUsers(int PageIndex, int PageSize)
        {
            int totalRecords;
            return System.Web.Security.Membership.GetAllUsers(PageIndex, PageSize, out totalRecords).Cast<MembershipUser>().ToList();
        }
    }
}