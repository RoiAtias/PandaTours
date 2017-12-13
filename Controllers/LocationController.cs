using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PandaTours.Models;
using PandaTours.DAL;

namespace PandaTours.Controllers
{
    /*Location Controller*/
    public class LocationController : Controller
    {
        /*db holder the project data-base*/
        private VacationDBContext db = new VacationDBContext();

        /*index method-return location table to index page*/
        // GET: /Location/
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        /*index manager method-return location table to ManageLocation page*/
        // GET: /Location/
        public ActionResult ManageLocation()
        {
            return View(db.Locations.ToList());
        }

        /*Details method-get location id and reurn his data */
        // GET: /Location/Details/5
        public ActionResult Details(int? id)
        {
            //check if id is not null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id not null -get from db.Location the currect location by id 
            Location location = db.Locations.Find(id);
            //check if the location is null
            if (location == null)
            {
                return HttpNotFound();
            }
            //else return to view with location object 
            return View(location);
        }

        /*Create GET method-location create page */
        // GET: /Location/Create
        public ActionResult Create()
        {
            return View();
        }

        /*Create POST method-get location object */
        // POST: /Location/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,Name,Description,Latitude,Longitude,ZIndex")] Location location)
        {
            //check if location object is valid and then add him to db.Location, finally return to Index page
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        /*Edit GET method-get location id */
        // GET: /Location/Edit/5
        public ActionResult Edit(int? id)
        {
            //check if location's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if location's id is not null -get the currect location by id
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        /*Edit POST method-get location object */
        // POST: /Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,Name,Description,Latitude,Longitude,ZIndex")] Location location)
        {
            //check if location object is valid and then save the change in db.Location, finally return with the object after change to the same page
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(location);
        }

        /*Delete GET method-get location's id */
        // GET: /Location/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if location's id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if location's id is not null -get the currect location by id
            Location location = db.Locations.Find(id);
            //check if get null
            if (location == null)
            {
                return HttpNotFound();
            }
            //return to view with location object before delete him
            return View(location);
        }

        /*DeleteConfirmed POST method-get location's id */
        // POST: /Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the currect location by id and remove him from db, finally retrun to index page
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
