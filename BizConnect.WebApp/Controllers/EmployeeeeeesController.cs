using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizConnect.Data;
using BizConnect.WebApp.Models.Accounts;

namespace BizConnect.WebApp.Controllers
{

    public class EmployeeeeeesController : Controller
    {
        BizConnectContext Context = new BizConnectContext();
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(Context.Employee.AsEnumerable());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id)
        {
            return View(Context.Employee.Single(e => e.EmployeeId == id));
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View(new Employee());
        } 

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                Context.Employee.Add(emp);

                if (ModelState.IsValid)
                    Context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Employee/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(Context.Employee.Single(x => x.EmployeeId.Equals(id)));
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Employee emp)
        {
            try
            {
                var selectedEmployee = Context.Employee.Single(x => x.EmployeeId.Equals(id));

                selectedEmployee.Name = emp.Name;
                selectedEmployee.Address = emp.Address;
                selectedEmployee.Phone = emp.Phone;
                selectedEmployee.Email = emp.Email;
                selectedEmployee.Username = emp.Username;
                selectedEmployee.Password = emp.Password;
                selectedEmployee.IsAdmin = emp.IsAdmin;

                if (ModelState.IsValid)
                    Context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Employee emp)
        {
            try
            {
                Context.Employee.Remove(emp);

                if (ModelState.IsValid)
                    Context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
