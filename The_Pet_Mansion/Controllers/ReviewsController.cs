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
        public ActionResult Delete(int id)
        {
            Review rev = db.Reviews.Find(id);
            db.Reviews.Remove(rev);
            db.SaveChanges();
            return Redirect("/Products/Show/" + rev.ProductID);
        }

        [HttpPost]
        public ActionResult New(Review rev)
        {
            rev.Date = DateTime.Now;
            


            try
            { 
                
                 db.Reviews.Add(rev);
                 db.SaveChanges();
                 return Redirect("/Products/Show/" + rev.ProductID);
                
               
            }

            catch (Exception e)
            {
                return Redirect("/Products/Show/" + rev.ProductID);
            }

        }

        public ActionResult Edit(int id)
        {
            Review rev = db.Reviews.Find(id);
            
            return View(rev);
        }

        [HttpPut]
        public ActionResult Edit(int id, Review requestReview)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Review rev = db.Reviews.Find(id);
                    if (TryUpdateModel(rev))
                    {
                        rev.Content = requestReview.Content;
                        db.SaveChanges();
                        return Redirect("/Products/Show/"+ rev.ProductID);
                    }
                    else
                    {
                        return View(requestReview);
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