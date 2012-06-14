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

namespace BizConnect.WebApp.Controllers
{
    [Authorize(Roles = "Owner")]
    public class CompanyController : Controller
    {
        private BizConnectContext db = new BizConnectContext(); //;

        //
        // GET: /Company/

        public ViewResult Index()
        {
            return View(db.Companies.ToList());
        }

        //
        // GET: /Company/Details/5

        public ViewResult Details(int id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {

            if (ModelState.IsValid)
            {
                db.Employees.Find(Membership.GetUser().ProviderUserKey).Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }
        
        //
        // GET: /Company/Edit/5
 
        public ActionResult Edit(int id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5
 
        public ActionResult Delete(int id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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