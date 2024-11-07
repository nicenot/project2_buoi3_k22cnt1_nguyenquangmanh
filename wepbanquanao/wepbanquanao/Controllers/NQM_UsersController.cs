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
    public class NQM_UsersController : Controller
    {
        private wepbanquanaoEntities db = new wepbanquanaoEntities();

        // GET: NQM_Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: NQM_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: NQM_Users/Create
        // GET: NQM_Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NQM_Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,password,email,created_at,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem đã có người dùng với email hoặc username giống nhau chưa
                var existingUser = db.Users.FirstOrDefault(u => u.username == user.username || u.email == user.email);
                if (existingUser != null)
                {
                    // Nếu đã có, thông báo lỗi
                    ModelState.AddModelError("", "Username hoặc Email đã tồn tại.");
                    return View(user);
                }

                // Nếu không có lỗi, thêm người dùng vào cơ sở dữ liệu
                db.Users.Add(user);
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction("Index"); // Quay lại trang danh sách người dùng
            }

            // Nếu có lỗi trong ModelState, quay lại trang tạo
            return View(user);
        }


        // GET: NQM_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: NQM_Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,username,password,email,created_at,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: NQM_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: NQM_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
