
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizConnect.Data.Entities;

namespace BizConnect.WebApp.Models.Invoice
{
    public class InvoiceModel
    {
        public ICollection<Company> Companies { get; set; }
        public ICollection<Layout> Layouts { get; set; }
        public ICollection<InvoiceData> InvoiceData { get; set; }

    }
}