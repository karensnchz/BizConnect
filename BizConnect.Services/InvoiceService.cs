using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizConnect.Data;

namespace BizConnect.Services
{
    public interface IInvoiceService : IService<Invoice>
    {
    }

    public class InvoiceService: Service<Invoice, BizConnectEntities>, IInvoiceService
    {
        public InvoiceService(BizConnectEntities dbContext): base(dbContext)
        {

        }
    }
}
