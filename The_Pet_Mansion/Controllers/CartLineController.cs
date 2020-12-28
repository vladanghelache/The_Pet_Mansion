using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class CartLineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CartLine
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Roles = "Admin,Editor,User")]
        public ActionResult New(CartLine cart)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {

                    cart.Quantity = 1;
                    cart.CartID = User.Identity.GetUserId();
                    db.CartLines.Add(cart);
                    db.SaveChanges();
                    TempData["message"] = "Produsul a fost adaugat in cos!";
                    return Redirect("/Products/Index/");


                }

                catch (Exception e)
                {
                    return Redirect("/Products/Show/" + cart.ProductID);
                }
            }

            else
                return RedirectToAction("Register","Account");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           CartLine cart = db.CartLines.Find(id);
           
            return View(cart);

        }

        [HttpPut]
        public ActionResult Edit(int id, CartLine requestCartLine)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    CartLine cart = db.CartLines.Find(id);
                    if (TryUpdateModel(cart))
                    {
                        if(requestCartLine.Quantity <= cart.Product.Stock)
                        {
                            cart = requestCartLine;
                            db.SaveChanges();
                            return Redirect("/Cart/Show/" + cart.CartID);
                        }
                        else
                        {
                            TempData["message"] = "Nu sunt suficiente produse in stoc!";
                            if (TempData.ContainsKey("message"))
                            {
                                ViewBag.Message = TempData["message"];
                            }
                            return View(requestCartLine);
                        }
                       
                    }
                    else
                    {
                        return View(requestCartLine);
                    }
                }
                else
                {
                    return View(requestCartLine);
                }
            }

            catch (Exception e)
            {
                return View(requestCartLine);
            }


        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            CartLine cart = db.CartLines.Find(id);
            db.CartLines.Remove(cart);
            db.SaveChanges();
            TempData["message"] = "Produsul a fost eliminat din cos!";
            return Redirect("/Cart/Show1/");
        }

    }
}