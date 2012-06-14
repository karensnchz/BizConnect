using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizConnect.Services;
using BizConnect.WebApp.Models.Invoice;
using System.Text;
using System.Text.RegularExpressions;

namespace BizConnect.WebApp.Areas.Mobile.Controllers
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
        
        public JsonResult Index(int Id)
        {
            var model = DataService.Layouts.ByKey(l => l.Id == Id);

            return new JsonResult() { Data = new { data = model} };
        }

        public JsonResult Details(int Id)
        {
            var layout = DataService.Layouts.ByKey(l => l.Id == Id);
            var elements = layout == null ? "" : Encoding.Unicode.GetString(layout.Elements);
            return new JsonResult() { Data = new { data = elements } };
        }
    }
}
