using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }


        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        public ActionResult New()
        {
            return View();
        }




        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["message"] = "Categoria a fost adaugata cu succes!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(category);
                }


            }
            catch (Exception e)
            {
                return View(category);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);

        }

        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Category category = db.Categories.Find(id);
                    if (TryUpdateModel(category))
                    {
                        category = requestCategory;
                        db.SaveChanges();
                        TempData["message"] = "Categoria a fost modificata cu succes!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(requestCategory);
                    }
                }
                else
                {
                    return View(requestCategory);
                }
            }

            catch (Exception e)
            {
                return View();
            }


        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["message"] = "Categoria a fost eliminata!";
            return RedirectToAction("Index");
        }
    }
}