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
                TempData["message"] = "A aparut o eroare la crearea cosului!";
            }

        }

        public ActionResult Show(string id)
        {
            Cart cart = db.Carts.Find(id);
            if (cart.CartID == User.Identity.GetUserId() )
            {
                if (TempData.ContainsKey("message"))
                {
                    ViewBag.Message = TempData["message"];
                }
                return View(cart);

            }

            else
            {

                TempData["message"] = "Nu aveti dreptul sa vizualizati cosul altui user!";
                return Redirect("/Products/Index");

            }
            

            
        }

        public ActionResult Show1()
        {
            string id = User.Identity.GetUserId();
            return RedirectToAction("/Show/" + id);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Cart cart = db.Carts.Find(id);
            return View(cart);

        }

        [HttpPut]
        public ActionResult Edit(string id, Cart requestCart)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Cart cart = db.Carts.Find(id);
                    if (TryUpdateModel(cart))
                    {
                        cart = requestCart;
                        db.SaveChanges();
                        NewOrder();
                        
                        return Redirect("/Cart/Show/" + cart.CartID);
                    }
                    else
                    {
                        return View(requestCart);
                    }
                }
                else
                {
                    return View(requestCart);
                }
            }

            catch (Exception e)
            {
                return View(requestCart);
            }


        }

        [NonAction]
        public void NewOrder()
        {
            Exception totalPriceZero = new Exception();
            try
            {

                var Id = User.Identity.GetUserId();
                Cart cart = db.Carts.Find(Id);
                Order order = new Order();

                order.UserID = Id;
                order.Date = DateTime.Now;
                order.Address = cart.Address;
                order.PhoneNumber = cart.PhoneNumber;
                
                db.Orders.Add(order);
                db.SaveChanges();

               float total_price = 0;

                foreach (var x in cart.CartLines)
                {
                    OrderLine orderLine = new OrderLine();
                    orderLine.OrderID = order.OrderID;
                    orderLine.ProductID = x.ProductID;
                    orderLine.Quantity = x.Quantity;
                   
                    total_price = total_price + (x.Product.Price * x.Quantity);
                    db.OrderLines.Add(orderLine);
                    db.SaveChanges();

                }
                if(total_price == 0)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    throw totalPriceZero;
                }
                order.TotalPrice = total_price;
                db.SaveChanges();

                int n = cart.CartLines.Count();
                while( n-- > 0 )
                {
                    cart.CartLines.First().Product.Stock = cart.CartLines.First().Product.Stock - cart.CartLines.First().Quantity;
                    db.CartLines.Remove(cart.CartLines.First());
                    db.SaveChanges();
                }
                
                TempData["message"] = "Comanda a fost plasata!";


            }

            catch (Exception e)
            {
                TempData["message"] = "A aparut o eroare la trimiterea comenzii!";
            }

        }
    }
}