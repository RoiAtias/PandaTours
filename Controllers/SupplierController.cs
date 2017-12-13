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

namespace PandaTours.Controllers
{
    /*Supplier Controller*/
    public class SupplierController : Controller
    {
        /*db holder the project data-base*/
        private VacationDBContext db = new VacationDBContext();

        /*index method-include search action*/
        // GET: /Supplier/
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

            var supplier = from s in db.Suppliers//get customer from db.supplier
                           select s;
            //check if the search parameter in query is not null
            if (!String.IsNullOrEmpty(searchString))
            {//check if exist customers with name like search parameter
                supplier = supplier.Where(s => s.Destination == (searchString)
                                       || s.SupplierName == (searchString));
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    supplier = supplier.OrderByDescending(s => s.Destination);
                    break;
                case "Date":
                    supplier = supplier.OrderBy(s => s.SupplierName);
                    break;
                case "date_desc":
                    supplier = supplier.OrderByDescending(s => s.SupplierName);
                    break;
                default:  // Name ascending 
                    supplier = supplier.OrderBy(s => s.Destination);
                    break;
            }
            //determines how many supplier show in one page and number of page
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(supplier.ToPagedList(pageNumber, pageSize));
        }
        /*Details method-get supplier id and return his data */
        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {
            //check if id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id not null -get from db.supplier the currect supplier by id 
            Supplier supplier = db.Suppliers.Find(id);
            //check if the customer is null
            if (supplier == null)
            {

                return HttpNotFound();
            }
            //else return to view with supplier object 
            return View(supplier);
        }
        /*Create GET method-get supplier id and return create page */
        // GET: /Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        /*Create POST method-get supplier object */
        // POST: /Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,SupplierName,Address,Destination,Email,TelephoneSupplier")] Supplier supplier)
        {
            //check if supplier object is valid and then add him to db.Suppliers, finally return to supplier Index page
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }
        /*Edit GET method-get Supplier id */
        // GET: /Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            //check if Supplier's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if Supplier's id is not null -get the currect supplier by id
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        /*Edit POST method-get Supplier object */
        // POST: /Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,SupplierName,Address,Destination,Email,TelephoneSupplier")] Supplier supplier)
        {
            //check if supplier object is valid and then save the change in db.Suppliers, finally return to the same page with object after change
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        /*Delete GET method-get Suppliers id */
        // GET: /Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if Suppliers id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if supplier's id is not null -get the currect Supplier by id
            Supplier supplier = db.Suppliers.Find(id);
            //check if get null
            if (supplier == null)
            {
                return HttpNotFound();
            }
            //return to view with supplier object before delete him
            return View(supplier);
        }

        /*DeleteConfirmed POST method-get Supplier's id */
        // POST: /Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the currect supplier by id and remove him from db, finally retrun to index page
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
