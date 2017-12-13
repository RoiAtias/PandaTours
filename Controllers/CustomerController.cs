using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using PagedList;
using System.Web;
using System.Web.Mvc;
using PandaTours.Models;
using PandaTours.DAL;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;


namespace PandaTours.Controllers
{
    /*Customer Controller*/
    public class CustomerController : Controller
    {
        /*db holder the project data-base*/
        private VacationDBContext db = new VacationDBContext();

        /*index method-include search action*/
        // GET: /Customer/ 
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;//receives a sortOrder parameter from the query string
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//if sortOrder is empty then descending order by name
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //check if the search parameter in query is not null
            if (searchString != null)
            {
                page = 1;
            }
            else
            {//if search parameter in query is null
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;//currentFilter get search parameter

            var customer = from s in db.Customers//get customer from db.customers
                           select s;
            //check if the search parameter in query is not null
            if (!String.IsNullOrEmpty(searchString))
            {//check if exist customers with name like search parameter
                customer = customer.Where(s => (s.LastName).Equals(searchString)
                                       || (s.FirstName).Equals(searchString));
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    customer = customer.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    customer = customer.OrderBy(s => s.FirstName);
                    break;
                case "date_desc":
                    customer = customer.OrderByDescending(s => s.FirstName);
                    break;
                default:  // Name ascending 
                    customer = customer.OrderBy(s => s.LastName);
                    break;
            }
            //determines how many customer show in one page and number of page
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(customer.ToPagedList(pageNumber, pageSize));
        }

        /*Details method-get customer id and return his data */
        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            //check if id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id not null -get from db.customers the currect customer by id 
            Customer customer = db.Customers.Find(id);

            //check if the customer is null
            if (customer == null)
            {
                return HttpNotFound();
            }
            //else return to view with customer object 
            return View(customer);
        }

        /*Create GET method-get customer id and return create page */
        // GET: /Customer/Create
        public ActionResult Create(int? id)
        {
            return View();
        }


        /*Create POST method-get customer object */
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,PassportNum,FirstName,LastName,BirthDate,Address,PhoneNum,Email,JoinDate")] int id, Customer customer)
        {
            //check if customer object is valid and then add him to db.customers, finally return to create order page
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Create", "Order", new { id = id, c_id = customer.CustomerID });
            }

            return View(customer);
        }

        /*Edit GET method-get customer id */
        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            //check if customer's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if customer's id is not null -get the currect customer by id
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        /*Edit POST method-get customer object */
        // POST: /Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,PassportNum,FirstName,LastName,BirthDate,Address,PhoneNum,Email,JoinDate")] Customer customer)
        {
            //check if customer object is valid and then save the change in db.customers, finally return to the same page with object after change
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        /*Delete GET method-get customer's id */
        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if customer's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if customer's id is not null -get the currect customer by id
            Customer customer = db.Customers.Find(id);
            //check if get null
            if (customer == null)
            {
                return HttpNotFound();
            }
            //return to view with customer object before delete him
            return View(customer);
        }

        /*DeleteConfirmed POST method-get customer's id */
        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the currect customer by id and remove him from db, finally retrun to index page
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*Dispose method-call when finish use controller*/
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

