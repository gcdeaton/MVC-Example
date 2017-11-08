using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS397FinalProject.Models;

namespace CS397FinalProject.Controllers
{
    public class EmployeesController : Controller
    {
        private CS397FinalProjectContext db = new CS397FinalProjectContext();

        // GET: Employees
        public ActionResult Index(string ddlAttributes,string SearchValue,string sortOrder)
        {
            var Employees = from e in db.Employees select e;


            if (!String.IsNullOrEmpty(SearchValue) && !String.IsNullOrEmpty(ddlAttributes))
            {
                if(ddlAttributes == "LastName")
                {
                    Employees = Employees.Where(e => e.lastName.Contains(SearchValue));

                }
                else if(ddlAttributes == "FirstName")
                {
                    Employees = Employees.Where(e => e.firstName.Contains(SearchValue));
                }
                else if (ddlAttributes == "Salary")
                {
                    //Employees = Employees.Where(e => e.salary.Contains(SearchValue));
                }
                else if (ddlAttributes == "Gender")
                {
                    Employees = Employees.Where(e => e.gender.Contains(SearchValue));
                }
                else if(ddlAttributes == "Department")
                {
                    Employees = Employees.Where(e => e.department.Contains(SearchValue));
                }
                else if (ddlAttributes == "Location")
                {
                    Employees = Employees.Where(e => e.location.Contains(SearchValue));
                }
                else
                {
                    Employees = Employees.Where(e => e.performance.Contains(SearchValue));
                }
            }

                switch (sortOrder)
                {
                    case "FirstName":
                        Employees = Employees.OrderBy(e => e.firstName);
                        break;
                    case "LastName":
                        Employees = Employees.OrderBy(e => e.lastName);
                        break;
                    case "Department":
                        Employees = Employees.OrderBy(e => e.department);
                        break;
                    case "Performance":
                        Employees = Employees.OrderBy(e => e.performance);
                        break;
                    default:
                        break;
                }
            


            var attributes = new SelectList(new[]
            {
                new{ ID="LastName", Name = "Last Name"},
                new{ ID="FirstName", Name = "First Name"},
                new{ ID="Salary", Name = "Salary"},
                new{ ID="Gender", Name = "Gender"},
                new{ ID="Department", Name = "Department"},
                new{ ID="Location", Name = "Location"},
                new{ ID="Performance", Name = "Performance"}, }, "ID", "Name", 0);


            
           
            
           



            ViewBag.ddlAttributes = attributes;


            return View(Employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,lastName,firstName,salary,gender,department,location,performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,lastName,firstName,salary,gender,department,location,performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ddlEdit()
        {
            var Employees = from e in db.Employees select e;
           
            return View(Employees); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
