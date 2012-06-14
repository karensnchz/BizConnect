using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizConnect.WebApp.Models.Invoice
{
    public class InvoiceFillModel
    {
        public List<ClientModel> clientModel = new List<ClientModel>();
        //public DataInvoiceModel dataInvoiceModel = new DataInvoiceModel();
        public List<LayoutModel> layoutModel = new List<LayoutModel>();

        public InvoiceFillModel()
        {

        }

        public InvoiceFillModel(List<ClientModel> cm, DataInvoiceModel dim, List<LayoutModel> lm)
        {
            clientModel = cm;
            //dataInvoiceModel = dim;
            layoutModel = lm;
        }
    }
}