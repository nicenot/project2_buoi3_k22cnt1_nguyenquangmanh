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
    public class NQM_Order_ItemsController : Controller
    {
        private wepbanquanaoEntities db = new wepbanquanaoEntities();

        // GET: NQM_Order_Items
        public ActionResult Index()
        {
            var order_Items = db.Order_Items.Include(o => o.Order).Include(o => o.Product);
            return View(order_Items.ToList());
        }

        // GET: NQM_Order_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // GET: NQM_Order_Items/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.Orders, "order_id", "order_id");
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name");
            return View();
        }

        // POST: NQM_Order_Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_item_id,order_id,product_id,quantity,price")] Order_Items order_Items)
        {
            if (ModelState.IsValid)
            {
                db.Order_Items.Add(order_Items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.Orders, "order_id", "order_id", order_Items.order_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", order_Items.product_id);
            return View(order_Items);
        }

        // GET: NQM_Order_Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.Orders, "order_id", "order_id", order_Items.order_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", order_Items.product_id);
            return View(order_Items);
        }

        // POST: NQM_Order_Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_item_id,order_id,product_id,quantity,price")] Order_Items order_Items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.Orders, "order_id", "order_id", order_Items.order_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", order_Items.product_id);
            return View(order_Items);
        }

        // GET: NQM_Order_Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // POST: NQM_Order_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Items order_Items = db.Order_Items.Find(id);
            db.Order_Items.Remove(order_Items);
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
