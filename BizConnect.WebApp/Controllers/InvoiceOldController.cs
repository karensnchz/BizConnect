using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizConnect.Services;
using System.Text.RegularExpressions;
using BizConnect.WebApp.Models.Invoice;
using System.Text;
using BizConnect.Data;

namespace BizConnect.WebApp.Controllers
{
    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/

        public IDataServices DataService { get; set; }

        public InvoiceController(IDataServices dataService)
        {
            DataService = dataService;
        }


        public ActionResult Index()
        {
            return View(DataService.Invoice.All().ToList());
        }

        public ActionResult Create()
        {
            var model = new InvoiceFillModel();

            foreach (var l in DataService.Layouts.All())
            {

                model.layoutModel.Add(new LayoutModel() { Name = l.Name, Elements = Encoding.Unicode.GetString(l.Elements), Id = l.Id });
            }

            foreach (var c in DataService.Client.All())
            {
                model.clientModel.Add(new ClientModel() { Id = c.Id, Name = c.Name, PhoneNumber = c.Telephone, Address = c.Address, Email = c.Email, Birthday = c.Birthday, Cell = c.Cell_Phone });
            }

            return View(model);
        }

        //Add information to the layout
        [HttpPost]
        public JsonResult Create(string Tablename, int ClientId, List<string[]> TableValue, string[] ColumnName, string[] ColumnValue )
        {
            string msg = "Data Saved";
            var ListData = new List<InvoiceData>();

            if (ColumnName.Length != ColumnValue.Length)
            {
                msg = "Missing Values";
            }
            else
            {
                var invoice = new Data.Invoice()
                {
                    Client_Id = ClientId,
                    Payment_Status = false,
                };

                foreach (var l in TableValue)
                    ListData.Add(new Data.InvoiceData()
                    {
                        Invoice = invoice,
                        Description = l[0],
                        Labor = decimal.Parse(l[1]),
                        Tax = decimal.Parse(l[2]),
                        Total = decimal.Parse(l[3])
                    });

                foreach (var i in ListData)
                {
                    DataService.InvoiceData.Add(i);
                }
                
                DataService.Invoice.Add(invoice);


                try
                {
                    StringBuilder query = new StringBuilder("INSERT INTO [" + Tablename + "] (Id, ");

                    int i = 0;
                    for (; i < ColumnName.Length - 1; i++)
                    {
                        query.AppendFormat("[{0}], ", ColumnName[i]);
                    }
                    query.AppendFormat("[{0}]) VALUES ({1}, ", ColumnName[i], ClientId);

                    for (i=0; i < ColumnValue.Length - 1; i++)
                    {
                        query.AppendFormat("'{0}', ", ColumnValue[i]);
                    }
                    query.AppendFormat("'{0}')", ColumnValue[i]);

                    DataService.DataContext.Database.ExecuteSqlCommand(query.ToString());
                }
                catch (Exception e)
                {
                    DataService.Invoice.Delete(invoice);
                    
                    foreach(var i in ListData)
                        DataService.InvoiceData.Delete(i);
                    
                    msg = e.Message;
                    Console.Write(msg);
                }

            }


            return new JsonResult() { Data = new { message = msg} };
        }

        public ActionResult Update(int Id)
        {
            var invoice = DataService.Invoice.ByKey(x => x.Id == Id);

            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }





    }
}
