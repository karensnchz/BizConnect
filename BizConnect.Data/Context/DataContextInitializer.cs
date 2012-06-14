using System.Data.Entity;
using System.Web.Security;

namespace BizConnect.Data.Context
{ 
    public class DataContextInitializer:DropCreateDatabaseAlways<BizConnectContext>
    {
        protected override void Seed(BizConnectContext context)
        {
            Roles.CreateRole("Owner");
            Roles.CreateRole("Admin");
        }
    }
}