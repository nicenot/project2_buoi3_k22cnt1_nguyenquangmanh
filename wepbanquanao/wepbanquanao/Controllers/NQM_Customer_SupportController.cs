using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wepbanquanao.Models;

namespace wepbanquanao.Controllers
{
    public class NQM_Customer_SupportController : Controller
    {
        private wepbanquanaoEntities db = new wepbanquanaoEntities();

        // GET: NQM_Customer_Support
        public ActionResult Index()
        {
            var customer_Support = db.Customer_Support.Include(c => c.User);
            return View(customer_Support.ToList());
        }

        // GET: NQM_Customer_Support/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Support customer_Support = db.Customer_Support.Find(id);
            if (customer_Support == null)
            {
                return HttpNotFound();
            }
            return View(customer_Support);
        }

        // GET: NQM_Customer_Support/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        // POST: NQM_Customer_Support/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "support_id,user_id,issue_description,status,created_at")] Customer_Support customer_Support)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Support.Add(customer_Support);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", customer_Support.user_id);
            return View(customer_Support);
        }

        // GET: NQM_Customer_Support/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Support customer_Support = db.Customer_Support.Find(id);
            if (customer_Support == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", customer_Support.user_id);
            return View(customer_Support);
        }

        // POST: NQM_Customer_Support/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "support_id,user_id,issue_description,status,created_at")] Customer_Support customer_Support)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Support).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", customer_Support.user_id);
            return View(customer_Support);
        }

        // GET: NQM_Customer_Support/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Support customer_Support = db.Customer_Support.Find(id);
            if (customer_Support == null)
            {
                return HttpNotFound();
            }
            return View(customer_Support);
        }

        // POST: NQM_Customer_Support/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Support customer_Support = db.Customer_Support.Find(id);
            db.Customer_Support.Remove(customer_Support);
            db.SaveChanges();
            return RedirectToAction("Index");
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
