using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BizConnect.Data.Entities;
using BizConnect.Data.Context;
using BizConnect.WebApp.Helpers;
using BizConnect.WebApp.Models;
using BizConnect.WebApp.Models.Accounts;

namespace BizConnect.WebApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private BizConnectContext db = new BizConnectContext();

        //
        // GET: /Employee/

        public ViewResult Index()
        {
            var employee = db.Employees.Include(e => e.Account);
            return View(employee.ToList());
        }

        //
        // GET: /Employee/Details/5

        public ViewResult Details(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            //ViewBag.EmployeeId = new SelectList(db.Users, "UserId", "Username");
            var companies = db.Employees.Find(Membership.GetUser().ProviderUserKey).Companies;

            return View(new EmployeeModel {Companies = companies});
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                Utility.Register(new RegisterModel
                {
                    ConfirmPassword = employee.ConfirmPassword,
                    Email = employee.Email,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Password = employee.Password,
                    UserName = employee.UserName
                }, employee.IsAdmin ? UserType.Admin : UserType.Employee, db.Companies.Find(employee.SelectedCompany), false, db);

                //Utility.Register(employee.Employee, employee.IsAdmin ? UserType.Admin : UserType.Employee, employee.SelectedCompany, false);
                return RedirectToAction("Index");
            }

            //ViewBag.EmployeeId = new SelectList(db.Users, "UserId", "Username", employee.EmployeeId);
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            //ViewBag.EmployeeId = new SelectList(db.Users, "UserId", "Username", employee.EmployeeId);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "UserId", "Username", employee.EmployeeId);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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