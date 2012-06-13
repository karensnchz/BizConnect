using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizConnect.Data;

namespace BizConnect.Services
{
    public interface IInvoiceDataService : IService<InvoiceData>
    {
    }

    public class InvoiceDataService: Service<InvoiceData, BizConnectEntities>,IInvoiceDataService
    {
        public InvoiceDataService(BizConnectEntities dbContext)
            : base(dbContext)
        {
        }
    }
}
