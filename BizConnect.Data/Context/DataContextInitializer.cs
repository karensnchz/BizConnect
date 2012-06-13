using System.Data.Entity;
using System.Web.Security;

namespace BizConnect.Data.Context
{ 
    public class DataContextInitializer:DropCreateDatabaseAlways<BizConnectContext>
    {
        protected override void Seed(BizConnectContext context)
        {
        MembershipCreateStatus Status;
        System.Web.Security.Membership.CreateUser("Demo", "123456", "demo@demo.com", null, null, true, out Status);
        Roles.CreateRole("Admin");
        Roles.AddUserToRole("Demo", "Admin");
        }
    }
}