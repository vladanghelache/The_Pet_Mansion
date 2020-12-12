using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Roles ="Admin,Editor,User")]
        public ActionResult Delete(int id)
        {
            Review rev = db.Reviews.Find(id);

            if (rev.UserID == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                
                db.Reviews.Remove(rev);
                db.SaveChanges();
                return Redirect("/Products/Show/" + rev.ProductID);
            }
            else
            {

                TempData["message"] = "Nu aveti dreptul sa stergeti un review care nu va apartine!";
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult New(Review rev)
        {
            rev.Date = DateTime.Now;
            


            try
            {

                 rev.UserID = User.Identity.GetUserId();
                 db.Reviews.Add(rev);
                 db.SaveChanges();
                 return Redirect("/Products/Show/" + rev.ProductID);
                
               
            }

            catch (Exception e)
            {
                return Redirect("/Products/Show/" + rev.ProductID);
            }

        }
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Edit(int id)
        {
            Review rev = db.Reviews.Find(id);
            if (rev.UserID == User.Identity.GetUserId() || User.IsInRole("Admin"))
                return View(rev);
            else
            {

                TempData["message"] = "Nu aveti dreptul sa editati un review care nu va apartine!";
                return RedirectToAction("Index");

            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Edit(int id, Review requestReview)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Review rev = db.Reviews.Find(id);
                    if (rev.UserID == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                        if (TryUpdateModel(rev))
                        {
                            rev.Content = requestReview.Content;
                            db.SaveChanges();
                            return Redirect("/Products/Show/" + rev.ProductID);
                        }
                        else
                        {
                            return View(requestReview);
                        }
                    }
                    else
                    {

                        TempData["message"] = "Nu aveti dreptul sa editati un review care nu va apartine!";
                        return RedirectToAction("Index");

                    }

                }
                else
                {
                    return View(requestReview);
                }
            }
            catch (Exception e)
            {
                return View();
            }

        }
    }
}