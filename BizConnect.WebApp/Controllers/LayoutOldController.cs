using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizConnect.Services;
using BizConnect.WebApp.Models.Invoice;
using System.Text;
using System.Text.RegularExpressions;

namespace BizConnect.WebApp.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Layout/

        public IDataServices DataService { get; set; }

        public LayoutController(IDataServices dataService)
        {
            DataService = dataService;
        }
        
        public ActionResult Index(int Id)
        {
            DataService.Layouts.ByKey(l => l.Id == Id);
            
            return View();
        }

        public ActionResult Create()
        {
            //var model = new InvoiceFillModel();

            //foreach (var l in DataService.Layouts.All())
            //{

            //    model.layoutModel.Add(new LayoutModel() { Name = l.Name, Elements = Encoding.Unicode.GetString(l.Elements), Id = l.Id });
            //}

            //foreach (var c in DataService.Client.All())
            //{
            //    model.clientModel.Add(new ClientModel() { Id = c.Id, Name = c.Name, PhoneNumber = c.Telephone, Address = c.Address, Email = c.Email, Birthday = c.Birthday, Cell = c.Cell_Phone });
            //}

            //return View(model);

            return View();
        }

        public JsonResult Details(int Id)
        {
            var layout = DataService.Layouts.ByKey(l => l.Id == Id);
            var elements = layout == null ? "" : Encoding.Unicode.GetString(layout.Elements);
            return new JsonResult() { Data = new { data = elements } };
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string name, string Layout, string[] colName, string[] colType)
        {
            string msg = "Layout Saved Sucesfully";
            string[] temp = colName.Distinct().ToArray();

            if (colName.Length != temp.Length)
                msg = "Duplicate Elemets, please remove";
            else
            {
                var layout = new Data.Layout()
                {
                    Company_Id = 1,
                    Name = name,
                    Elements = System.Text.Encoding.Unicode.GetBytes(Regex.Replace(Regex.Replace(Layout, "(\\W)\\s+", "$1"), "\\s\\s+", " "))
                };

                try
                {
                    DataService.Layouts.Add(layout);


                    if (colName != null && colName.Length > 0)
                    {
                        StringBuilder query = new StringBuilder("CREATE TABLE [" + name + "] ( Id int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Clients(Id) ON DELETE CASCADE,");
                        int i = 0;
                        for (; i < colName.Length - 1; i++)
                        {
                            query.AppendFormat("[{0}] {1},", colName[i], colType[i]);
                        }

                        query.AppendFormat("[{0}] {1})", colName[i], colType[i]);

                        DataService.DataContext.Database.ExecuteSqlCommand(query.ToString());
                    }

                }
                catch (Exception e)
                {
                    DataService.Layouts.Delete(layout);
                    msg = e.Message;
                    Console.WriteLine(msg);
                }
            }
            return new JsonResult() { Data = new { message = msg } };

        }
 
    }
}
