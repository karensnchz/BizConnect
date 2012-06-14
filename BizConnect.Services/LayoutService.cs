using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizConnect.Data;

namespace BizConnect.Services
{
    public interface ILayoutService: IService<Layout>
    {
    }

    public class LayoutService: Service<Layout, BizConnectEntities>, ILayoutService
    {
        public LayoutService(BizConnectEntities dbContext): base(dbContext)
        {
            
        }
    }

    
}
