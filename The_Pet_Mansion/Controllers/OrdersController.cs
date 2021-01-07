using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {


            return View();
        }

        //show pentru afisarea tuturor comenzilor unui utilizator
        public ActionResult Show(string Id)
        {
            var orders = (from x in db.Orders
                         where x.UserID == Id
                         select x).ToList();
            ViewBag.Orders = orders;
            return View();
        }

        //folosit in Layout
        public ActionResult Show_redirect()
        {
            string UserId = User.Identity.GetUserId();
            return Redirect("/Orders/Show/" + UserId);
        }

        //show pentru afisarea unei comenzi cu Id-ul dat ca parametru
        public ActionResult Show_order(int Id)
        {
            Order order = db.Orders.Find(Id);
            return View(order);
        }


        


    }
    
}