using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using PandaTours.Models;
using PandaTours.DAL;

namespace PandaTours.Controllers
{
    /*VacationPackage Controller*/
    public class VacationPackageController : Controller
    {
        /*db holder the project data-base*/
        private VacationDBContext db = new VacationDBContext();

        /*index method-include search action*/
        // GET: /VacationPackage/
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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
            var vacationPackage = from s in db.VacationPackages//get vacationPackage from db.VacationPackages
                                  select s;
            //check if the search parameter in query is not null
            if (!String.IsNullOrEmpty(searchString))
            {//check if exist vacationPackage with name like search parameter
                vacationPackage = vacationPackage.Where(s => s.Destination.Contains(searchString)
                                       || s.Hotel.Contains(searchString));
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    vacationPackage = vacationPackage.OrderByDescending(s => s.Destination);
                    break;
                case "Date":
                    vacationPackage = vacationPackage.OrderBy(s => s.Hotel);
                    break;
                case "date_desc":
                    vacationPackage = vacationPackage.OrderByDescending(s => s.Hotel);
                    break;
                default:  // Name ascending 
                    vacationPackage = vacationPackage.OrderBy(s => s.Destination);
                    break;
            }
            //determines how many vacationPackage show in one page and number of page
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(vacationPackage.ToPagedList(pageNumber, pageSize));
        }

        /*Details method-get vacationPackage id and return his data */
        // GET: /VacationPackage/Details/5
        public ActionResult Details(int id = 0)
        {
            //get from db.VacationPackage the currect vacation-Package by id 
            VacationPackage vacationpackage = db.VacationPackages.Find(id);
            if (vacationpackage == null)
            {
                return HttpNotFound();
            }
            return View(vacationpackage);
        }

        /*Create GET method-get VacationPackage id and return create page */
        // GET: /VacationPackage/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        /*Create POST method-get VacationPackage object and file*/
        // POST: /VacationPackage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacationPackage vacationpackage, HttpPostedFileBase file)
        {
            //check if VacationPackage object is valid and then add him to db.Suppliers, finally return to supplier Index page
            if (ModelState.IsValid)
            {
                //check if file is not null
                if (file != null)
                {
                    //save upload image in project and his name in db
                    file.SaveAs(HttpContext.Server.MapPath("~/img/") + file.FileName);
                    vacationpackage.Image = file.FileName;
                }
                //add package to db.VacationPackage and return to Index page
                db.VacationPackages.Add(vacationpackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", vacationpackage.SupplierID);
            return View(vacationpackage);
        }

        /*Edit GET method-get VacationPackage id */
        // GET: /VacationPackage/Edit/5
        public ActionResult Edit(int id = 0)
        {
            //get the currect vacationpackage by id
            VacationPackage vacationpackage = db.VacationPackages.Find(id);
            //check if vacationpackage is null
            if (vacationpackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", vacationpackage.SupplierID);
            return View(vacationpackage);
        }

        /*Edit POST method-get VacationPackage object */
        // POST: /VacationPackage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacationPackage vacationpackage, HttpPostedFileBase file)
        {
            //check if VacationPackage object is valid and then save the change in db.VacationPackage, finally return to the same page with object after change
            if (ModelState.IsValid)
            {
                db.Entry(vacationpackage).State = EntityState.Modified;
                //check if file is not null-save change and return to Index page
                if (file != null)
                {
                    //save upload image in project and his name in db
                    file.SaveAs(HttpContext.Server.MapPath("~/img/") + file.FileName);
                    vacationpackage.Image = file.FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", vacationpackage.SupplierID);
            return View(vacationpackage);
        }

        /*Delete GET method-get VacationPackage id */
        // GET: /VacationPackage/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if VacationPackage id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if VacationPackage's id is not null -get the currect VacationPackage by id
            VacationPackage vacationpackage = db.VacationPackages.Find(id);
            //check if get null
            if (vacationpackage == null)
            {
                return HttpNotFound();
            }
            return View(vacationpackage);
        }

        /*DeleteConfirmed POST method-get VacationPackage's id */
        // POST: /VacationPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the currect VacationPackage by id and remove him from db, finally retrun to index page
            VacationPackage vacationpackage = db.VacationPackages.Find(id);
            db.VacationPackages.Remove(vacationpackage);
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

        /*Deals method-retrun all suppliers's packages to Deal page*/
        public ActionResult Deals()
        {
            var vacationpackages = db.VacationPackages.Include(v => v.supplier);
            return View(vacationpackages.ToList());
        }

        /*Search method-get 3 search parameters and pagging parameters*/
        public ActionResult Search(string Destination, string Hotel, int NightNum, int? page, string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;//receives a sortOrder parameter from the query string
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//if sortOrder is empty then descending order by nam
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //get Vacation from db.VacationPackages
            var Vacation = from s in db.VacationPackages select s;
            //check if search parameter-Destination is not null
            if (Destination != null)
            {
                //find the VacationPackages that include search parameter-Destination
                Vacation = Vacation.Where(s => s.Destination.Equals(Destination));
            }
            //check if search parameter-Hotel is not null
            if (Hotel != null)
            {
                //after find the VacationPackages with this destination find that are include search parameter-Hotel
                Vacation = Vacation.Where(s => s.Hotel.Equals(Hotel));
            }
            //check if search parameter-Night number is positive
            if (NightNum > 0)
            {
                //after find the VacationPackages with this destination and Hotel, find that are include search parameter-Night number
                Vacation = Vacation.Where(s => (s.NightNum) == (NightNum));
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    Vacation = Vacation.OrderByDescending(s => s.Destination);
                    break;
                case "Date":
                    Vacation = Vacation.OrderBy(s => s.Destination);
                    break;
                case "date_desc":
                    Vacation = Vacation.OrderByDescending(s => s.Destination);
                    break;
                default:  // Name ascending 
                    Vacation = Vacation.OrderBy(s => s.Destination);
                    break;
            }
            //determines how many customer show in one page and number of page
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View("Index", Vacation.ToPagedList(pageNumber, pageSize));
        }

        /*SearchDeals method-get 3 search parameters and return result*/
        public ActionResult SearchDeals(string Destination, string Hotel, int NightNum)
        {
            //get Vacation from db.VacationPackages
            var Vacation = from s in db.VacationPackages select s;
            //check if search parameter-Destination is not null
            if (Destination != null)
            {
                //find the VacationPackages that include search parameter-Destination
                Vacation = Vacation.Where(s => s.Destination.Equals(Destination));
            }
            //check if search parameter-Hotel is not null
            if (Hotel != null)
            {
                //after find the VacationPackages with this destination find that are include search parameter-Hotel
                Vacation = Vacation.Where(s => s.Hotel.Equals(Hotel));
            }
            //check if search parameter-Night number is positive
            if (NightNum > 0)
            {
                //after find the VacationPackages with this destination and Hotel, find that are include search parameter-Night number
                Vacation = Vacation.Where(s => (s.NightNum) == (NightNum));
            }
            //return result toDeals page
            return View("Deals", Vacation);
        }

        /*vacationPackagePrice method-join action*/
        public ActionResult vacationPackagePrice()
        {
            //create VacationPackagePriceResult object by find all orders that are total price bigger than 499.9
            var item = from Vac in db.VacationPackages
                       join Ord in db.Orders on Vac.VacationPackageID equals Ord.VacationPackageID
                       where Ord.Total > 499.9
                       select new VacationPackagePriceResult
                       {
                           CustomerID = Ord.CustomerID,
                           PassportNum = Ord.Customer.PassportNum,
                           FirstName = Ord.Customer.FirstName,
                           LastName = Ord.Customer.LastName,
                           VacationPackageID = Ord.VacationPackageID,
                           Hotel = Vac.Hotel,
                           PassengersNum = Ord.PassengersNum,
                           NightNum = Vac.NightNum,
                           Destination = Vac.Destination,
                           Total = Ord.Total
                       };
            //return all result after distinct
            return View(item.Distinct());
        }


        /*OrderDistGroupBy method-group by action*/
        public ActionResult OrderDistGroupBy()
        {
            //create OrderDistResult object by find all vacation-package that are night number bigger than 4
            var item = from Vac in db.VacationPackages
                       join Ord in db.Orders on Vac.VacationPackageID equals Ord.VacationPackageID
                       where Vac.NightNum > 4
                       group Ord by new { Vac.Destination, Ord.Total, Ord.CustomerID, Ord.Customer } into guip//order the result by:Destination,Total,CustomerID
                       select new OrderDistResult
                       {
                           Destination = guip.Key.Destination,
                           CustomerID = guip.Key.Customer.CustomerID,
                           PassportNum = guip.Key.Customer.PassportNum,
                           FirstName = guip.Key.Customer.FirstName,
                           LastName = guip.Key.Customer.LastName,
                           Total = guip.Key.Total
                       };
            //return all result after distinct
            return View(item.Distinct());
        }
    }
}