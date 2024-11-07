using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wepbanquanao.Areas.ADMIN
{
    public class Home_AdminController : Controller
    {
        // GET: ADMIN/Home_Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(String user, String password) { 
        
            // check code 
            if(user.ToLower() == "admin" && password == "123456")
            {
                Session["user"] = "admin";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }



        
        
        }

        
    }
}