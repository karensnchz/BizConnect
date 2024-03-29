﻿using System;
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
using BizConnect.WebApp.Models.Invoice;

namespace BizConnect.WebApp.Controllers
{ 
    public class InvoiceController : Controller
    {
        private BizConnectContext db = new BizConnectContext();

        //
        // GET: /Invoice/
        public ViewResult Index()
        {
            return View(db.Invoices.ToList());
        }

        //
        // GET: /Invoice/Details/5
        public ViewResult Details(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            return View(invoice);
        }

        //
        // GET: /Invoice/Create
        public ActionResult Create()
        {
            var user = db.Employees.Find(Membership.GetUser().ProviderUserKey);
            var layouts = new List<Layout>();
            foreach (var result in user.Companies.Select(c => c.Layouts))
                layouts.AddRange(result);
            return View(new InvoiceModel{Companies = user.Companies, Layouts = layouts});
        } 

        //
        // POST: /Invoice/Create
        [HttpPost]
        public ActionResult Create(ICollection<InvoiceData> data, int layoutId)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(new Invoice{Data = data, LayoutId = layoutId});
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return RedirectToAction("Create");
        }
        
        //
        // GET: /Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult RenderLayout(int layoutId)
        {
            return PartialView("Test", db.Layouts.Find(layoutId));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}