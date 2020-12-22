using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

       
        [NonAction]
        public void New(string id)
        {
            Cart cart = new Cart();

            try
            {

                
                cart.CartID = id;
                db.Carts.Add(cart);
                db.SaveChanges();
                
               


            }

            catch (Exception e)
            {
                TempData["message"] = "A aparut o eroare la crearea cosuluis!";
            }

        }

        public ActionResult Show(string id)
        {
            Cart cart = db.Carts.Find(id);

            return View(cart);
        }

        public ActionResult Show1()
        {
            string id = User.Identity.GetUserId();
            return RedirectToAction("/Show/" + id);
        }
    }
}