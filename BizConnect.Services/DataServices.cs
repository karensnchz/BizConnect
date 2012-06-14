using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BizConnect.Data;

namespace BizConnect.Services
{
    public interface IDataServices
    {
        //TODO Create the services for the other tables like company, etc etc
        ILayoutService Layouts { get; }
        IInvoiceDataService InvoiceData{get;}
        IClientService Client{get;}
        IInvoiceService Invoice { get; }
        BizConnectEntities DataContext { get; }


    }

    public class DataServices : IDataServices
    {
        public ILayoutService Layouts { get; private set; }
        public IInvoiceDataService InvoiceData { get; private set; }
        public IClientService Client { get; private set; }
        public IInvoiceService Invoice { get; private set; }
        public BizConnectEntities DataContext { get; private set; }
        

        public DataServices(DbContext ctx)
        {
            DataContext = ctx as BizConnectEntities;
            Layouts = new LayoutService(DataContext);
            InvoiceData = new InvoiceDataService(DataContext);
            Client = new ClientService(DataContext);
            Invoice = new InvoiceService(DataContext);
        }
    }
}
