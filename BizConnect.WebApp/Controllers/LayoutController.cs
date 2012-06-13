using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BizConnect.Data;
using BizConnect.Data.Context;
using BizConnect.Data.Entities;
using BizConnect.WebApp.Helpers;
using BizConnect.WebApp.Models;

namespace BizConnect.WebApp.Controllers
{
    [Authorize]
    public class LayoutController : Controller
    {
        private BizConnectContext db = new BizConnectContext();//new BizConnectContext(); //;

        //
        // GET: /Layout/

        public ViewResult Index()
        {
            return View(db.Layouts.Where(l => !l.Disabled));
        }

        //
        // GET: /Layout/Details/5

        public ViewResult Details(int id)
        {
            Layout layout = db.Layouts.Find(id);
            return View("Crud", new LayoutModel
            {
                Layout = layout,
                Companies = new[] { layout.Company },
                OperationType = OperationType.Details
            });
        }

        //
        // GET: /Layout/Create

        public ActionResult Create()
        {
            var userId = (Guid)Membership.GetUser().ProviderUserKey;

            return View("Crud", new LayoutModel
            {
                Layout = new Layout(),
                Companies = db.Companies.Where(c => userId == c.OwnerId),
                OperationType = OperationType.Create
            });
        }

        //
        // POST: /Layout/Create

        [HttpPost]
        public ActionResult Create(Layout layout, int companyId)
        {
            layout.Company = db.Companies.Find(companyId);
            if (ModelState.IsValid)
            {
                db.Layouts.Add(layout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userId = (Guid)Membership.GetUser().ProviderUserKey;

            return View("Crud", new LayoutModel
                                    {
                                        Layout = layout,
                                        Companies = db.Companies.Where(c => userId == c.OwnerId),
                                        OperationType = OperationType.Create
                                    });
        }



        //
        // GET: /Layout/Edit/5

        public ActionResult Edit(int id)
        {
            Layout layout = db.Layouts.Find(id);
            //layout.Elements = db.Element.Where(e => e.Layout.LayoutId == id).ToList();
            return View("Crud", new LayoutModel
                                    {
                                        Layout = layout,
                                        Companies = new[] { layout.Company },
                                        OperationType = OperationType.Edit
                                    });
        }

        //
        // POST: /Layout/Edit/5

        [HttpPost]
        public ActionResult Edit(Layout layout)
        {
            foreach (var element in layout.Elements)
            {
                if (element.ElementId == 0) db.Elements.Add(element);
                else db.Entry(element).State = EntityState.Modified;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Crud", new LayoutModel
                                    {
                                        Layout = layout,
                                        Companies = new[] { layout.Company },
                                        OperationType = OperationType.Edit
                                    });
        }

        //
        // GET: /Layout/Delete/5

        public ActionResult Delete(int id)
        {
            Layout layout = db.Layouts.Find(id);
            return View("Crud", new LayoutModel
            {
                Layout = layout,
                Companies = new[] { layout.Company },
                OperationType = OperationType.Delete
            });
        }

        //
        // POST: /Layout/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Layouts.Find(id).Disabled = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}