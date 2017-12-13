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
    /*Order Controller*/
    public class OrderController : Controller
    {
        /*db holder the project data-base*/
        private VacationDBContext db = new VacationDBContext();

        /*index method-include search action*/
        // GET: /Order/
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
            else//if search parameter in query is null
            {
                searchString = currentFilter;
            }
            //currentFilter get search parameter
            ViewBag.CurrentFilter = searchString;
            //get order from db.Orders
            var order = from s in db.Orders
                        select s;
            //check if the search parameter in query is not null
            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.Customer.PassportNum == (searchString));//check if exist order with passport number like search parameter
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    order = order.OrderByDescending(s => s.VacationPackage.Destination);
                    break;
                case "Date":
                    order = order.OrderBy(s => s.Customer.FirstName);
                    break;
                case "date_desc":
                    order = order.OrderByDescending(s => s.VacationPackage.Destination);
                    break;
                default:  // Name ascending 
                    order = order.OrderBy(s => s.Customer.FirstName);
                    break;
            }
            //determines how many customer show in one page and number of page
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(order.ToPagedList(pageNumber, pageSize));
        }

        /*Details method-get order id and return his data */
        // GET: /Order/Details/5
        public ActionResult Details(int? id)
        {
            //check if id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id not null -get from db.Orders the currect order by id 
            Order order = db.Orders.Find(id);
            //check if the order is null
            if (order == null)
            {
                return HttpNotFound();
            }
            //else return to view with order object 
            return View(order);
        }

        /*Create GET method-get package id and customer id, return create page */
        // GET: /Order/Create
        public ActionResult Create(int? id, int c_id)
        {
            //create new order
            Order order = new Order();
            //insert parameters into order object
            order.VacationPackageID = db.VacationPackages.Find(id).VacationPackageID;
            order.CustomerID = c_id;
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "PassportNum");
            ViewBag.VacationPackageID = new SelectList(db.VacationPackages, "VacationPackageID", "Destination");
            return View(order);

        }

        /*Create POST method-get order object */
        // POST: /Order/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,VacationPackageID,PassengersNum,Total,OrderDate")] Order order)
        {
            //check if order object is valid and then calculate total price, add him to db.Order and finally return to end order page
            if (ModelState.IsValid)
            {
                var singlePrice = db.VacationPackages.Find(order.VacationPackageID).SinglePay;
                order.Total = singlePrice * order.PassengersNum;
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("endOrder", "Order", new { id = order.OrderID });
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "PassportNum", order.CustomerID);
            ViewBag.VacationPackageID = new SelectList(db.VacationPackages, "VacationPackageID", "Destination", order.VacationPackageID);
            return View(order);
        }

        /*Edit GET method-get order id */
        // GET: /Order/Edit/5
        public ActionResult Edit(int? id)
        {
            //check if order's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if order's id is not null -get the currect order by id
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "PassportNum", order.CustomerID);
            ViewBag.VacationPackageID = new SelectList(db.VacationPackages, "VacationPackageID", "Destination", order.VacationPackageID);
            return View(order);
        }

        /*Edit POST method-get order object */
        // POST: /Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,VacationPackageID,PassengersNum,Total,OrderDate")] Order order)
        {
            //check if order object is valid and then save the change in db.Orders, finally return to the same page with object after change
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "PassportNum", order.CustomerID);
            ViewBag.VacationPackageID = new SelectList(db.VacationPackages, "VacationPackageID", "Destination", order.VacationPackageID);
            return View(order);
        }

        /*Delete GET method-get order's id */
        // GET: /Order/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if order's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if order's id is not null -get the currect order by id
            Order order = db.Orders.Find(id);
            //check if order is not null
            if (order == null)
            {
                return HttpNotFound();
            }
            //return to view with order object before delete him
            return View(order);
        }

        /*DeleteConfirmed POST method-get order's id */
        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        /*Search method-get 3 search parameters and pagging parameters*/
        // GET: /Order/Serach
        public ActionResult Search(string Destination, int? PassengersNum, string passportNum, int? page, string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;//receives a sortOrder parameter from the query string
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//if sortOrder is empty then descending order by nam
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //get order from db.Orders
            var order = from s in db.Orders select s;
            //check if search parameter-Destination is not null
            if (!String.IsNullOrEmpty(Destination))
            {
                //find the orders that include search parameter-Destination
                order = order.Where(s => (s.VacationPackage.Destination).Contains(Destination));
            }
            //check if search parameter-Passengers number is positive
            if (PassengersNum > 0)
            {
                //after find the orders with this destination find that are include search parameter-Passengers number
                order = order.Where(s => (s.PassengersNum) == (PassengersNum));
            }
            //check if search parameter-passport number is not null
            if (!String.IsNullOrEmpty(passportNum))
            {
                //after find the orders with this destination and Passengers number, find that are include search parameter-Passport number
                order = order.Where(s => (s.Customer.PassportNum).Equals(passportNum));
            }
            //determines the column which order by
            switch (sortOrder)
            {
                case "name_desc":
                    order = order.OrderByDescending(s => s.VacationPackage.Destination);
                    break;
                case "Date":
                    order = order.OrderBy(s => s.VacationPackage.Destination);
                    break;
                case "date_desc":
                    order = order.OrderByDescending(s => s.VacationPackage.Destination);
                    break;
                default:  // Name ascending 
                    order = order.OrderBy(s => s.VacationPackage.Destination);
                    break;
            }
            //determines how many customer show in one page and number of page
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View("Index", order.ToPagedList(pageNumber, pageSize));
        }

        /*OrderPassengersNum method-join action*/
        public ActionResult OrderPassengersNum()
        {
            //create OrderResult object by find all orders that include number of passangers number bigger than 3
            var item = from Vac in db.VacationPackages
                       join Ord in db.Orders on Vac.VacationPackageID equals Ord.VacationPackageID
                       where Ord.PassengersNum > 3
                       select new OrderResult
                       {
                           CustomerID = Ord.CustomerID,
                           PassportNum = Ord.Customer.PassportNum,
                           FirstName = Ord.Customer.FirstName,
                           LastName = Ord.Customer.LastName,
                           VacationPackageID = Ord.VacationPackageID,
                           Hotel = Vac.Hotel,
                           PassengersNum = Ord.PassengersNum,
                           Destination = Vac.Destination,
                           Total = Ord.Total,
                           NightNum = Vac.NightNum
                       };
            //return all result after distinct
            return View(item.Distinct());
        }

        /*endOrder method-get order id*/
        public ActionResult endOrder(int? id)
        {
            //check if id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if order's id is not null -get the currect order by id
            Order order = db.Orders.Find(id);
            //check if order is not null
            if (order == null)
            {
                return HttpNotFound();
            }
            //return order object to end order page
            return View(order);
        }
        /*Bonus method-return bouns page*/
        public ActionResult Bonus()
        {
            return View();
        }
        /*statistics method-return statistics page*/
        public ActionResult statistics()
        {
            return View();
        }

        /*getOrderInDestination method-using for create graph*/
        public JsonResult getOrderInDestination()
        {
            //create new string list
            var destinationList = new List<string>();
            //get from db.Orders all destination
            var destinationQry = from s in db.Orders orderby s.VacationPackage.Destination select s.VacationPackage.Destination;
            //add all destion from db to destination list after distinct
            destinationList.AddRange(destinationQry.Distinct());
            var random = new Random();
            //create new jason with destination list
            OrdersInDestination[] js = new OrdersInDestination[destinationList.Count()];
            int i = 0;
            //passing on each destination in list
            foreach (var item in destinationList)
            {
                //create new OrdersInDestination object
                OrdersInDestination obj = new OrdersInDestination();
                //set destination as label
                obj.label = item;
                //set value as 0
                obj.value = 0;
                //passing on each order in db
                foreach (var order in db.Orders)
                {
                    //check if order include item-destination
                    if (order.VacationPackage.Destination.Equals(item))
                    {
                        //update value
                        obj.value++;
                    }
                }
                //random color
                var color = String.Format("#{0:X6}", random.Next(0x1000000));
                //set color as random color
                obj.color = color.ToString();
                //set object in json
                js[i] = obj;
                //update index
                i++;
            }
            //return json 
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        /*getPassangeInOrder method-using for create graph*/
        public JsonResult getPassangeInOrder()
        {
            //create new string list
            var destinationList = new List<string>();
            //get from db.Orders all destination
            var destinationQry = from s in db.Orders orderby s.VacationPackage.Destination select s.VacationPackage.Destination;
            //add all destion from db to destination list after distinct
            destinationList.AddRange(destinationQry.Distinct());
            //create new jason with destination list
            PassangeInOrder[] js = new PassangeInOrder[destinationList.Count()];
            int i = 0;
            //passing on each destination in list
            foreach (var item in destinationList)
            {
                //create new OrdersInDestination object
                PassangeInOrder obj = new PassangeInOrder();
                //set destination as State
                obj.State = item;
                //set value as 0
                obj.freq = 0;
                //passing on each order in db
                foreach (var order in db.Orders)
                {
                    //check if order include item-destination
                    if (order.VacationPackage.Destination.Equals(item))
                    {
                        //update value to passanger number of this order, finally of all orders with this destination
                        obj.freq += order.PassengersNum;
                    }
                }
                //set object in json
                js[i] = obj;
                //update index
                i++;
            }
            //return json 
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }

    //define OrdersInDestination class- include 3 data members: lable,color,value
    public class OrdersInDestination
    {
        public string label { get; set; }
        public string color { get; set; }
        public int value { get; set; }

    }
    //define PassangeInOrder class- include 2 data members: State,freq
    public class PassangeInOrder
    {
        public string State { get; set; }
        public int freq { get; set; }
    }
}