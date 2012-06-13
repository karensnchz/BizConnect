using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizConnect.WebApp.Models.Invoice
{
    public class InvoiceCreateModel
    {
        public ClientModel clientModel = new ClientModel();
        public DataInvoiceModel dataInvoiceModel = new DataInvoiceModel();

        public InvoiceCreateModel(ClientModel cm, DataInvoiceModel dim)
        {
            clientModel = cm;
            dataInvoiceModel = dim;
        }

        public InvoiceCreateModel()
        {

        }
    }
}