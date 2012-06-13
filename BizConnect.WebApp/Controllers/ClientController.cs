using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizConnect.Services;
using BizConnect.WebApp.Models.Invoice;
using BizConnect.Data;
using System.Data.Entity;

namespace BizConnect.WebApp.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        //public IDataServices DataService { get; set; }


        //public ClientController(IDataServices dataService)
        //{
        //    DataService = dataService;
        //}

    //    public ActionResult Index()
    //    {
    //        return View(DataService.Client.All().Select(c => new ClientModel() { Id = c.Id, Name = c.Name, Address = c.Address, PhoneNumber = c.Telephone, Cell = c.Cell_Phone, Email = c.Email, Birthday = c.Birthday}));
        
    //    }

    //    public ActionResult Create()
    //    {
    //        return View(new ClientModel());
    //    }

    //    [HttpPost]
    //    public ActionResult Create(ClientModel cm)
    //    {
    //        DataService.Client.Add(new Client()
    //        {
    //            Name = cm.Name,
    //            Address = cm.Address,
    //            Telephone = cm.PhoneNumber,
    //            Cell_Phone = cm.Cell,
    //            Email = cm.Email,
    //            Birthday = cm.Birthday
    //        });

    //        return RedirectToAction("Index");
    //    }

        
    //    public JsonResult Display(int Id)
    //    {
    //        var client = DataService.Client.ByKey(c => c.Id == Id);
    //        return new JsonResult() { Data = new { 
    //            name = client.Name, 
    //            phone = client.Telephone, 
    //            address = client.Address, 
    //            email = client.Email, 
    //            birthday = client.Birthday.ToShortDateString() } 
    //        };
    //    }
    }
}
