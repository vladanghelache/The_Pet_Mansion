using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Products
        //comm
        public ActionResult Index()
        {
            var products = db.Products.Include("Category").Include("Animal");
         
            ViewBag.Products = products;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            // Extragem toate categoriile din baza de date
            var categories = from cat in db.Categories select cat;
            // iteram prin categorii
            foreach (var category in categories)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = category.CategoryID.ToString(),
                    Text = category.CategoryName.ToString()
                });
            }
            // returnam lista de categorii
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllAnimals()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            // Extragem toate categoriile din baza de date
            var animals = from anim in db.Animals select anim;
            // iteram prin categorii
            foreach (var anim in animals)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = anim.AnimalID.ToString(),
                    Text = anim.Species.ToString()
                });
            }
            // returnam lista de categorii
            return selectList;
        }

        public ActionResult New()
        {
            Product product = new Product();
            // preluam lista de categorii din metoda GetAllCategories()
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();
            return View(product);
        }

        [HttpPost]
        public ActionResult New(Product product)
        {
            product.Date = DateTime.Now;
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();

            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    TempData["message"] = "Produsul a fost adaugat cu succes!";
                    return RedirectToAction("Index");
                }

                else
                {
                    return View(product);
                }

            }
            catch (Exception e)
            {
                return View(product);
            }
        }

        public ActionResult Show (int id)
        {
            Product product = db.Products.Find(id);

            return View(product);
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            Product product = db.Products.Find(id);
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();

            return View(product);

        }

        [HttpPut]
        public ActionResult Edit (int id, Product requestProduct)
        {

            requestProduct.Categories = GetAllCategories();
            requestProduct.Animals = GetAllAnimals();

            try
            {
                if (ModelState.IsValid)
                {
                    Product product = db.Products.Find(id);
                    if (TryUpdateModel(product))
                    {
                        product = requestProduct;
                        db.SaveChanges();
                        TempData["message"] = "Produsul a fost modificat cu succes!";
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        return View(requestProduct);
                    }
                }

                else
                {
                    return View(requestProduct);
                }
            }

            catch (Exception e)
            {
                return View();
            }

            
        }

        [HttpDelete]
        public ActionResult Delete (int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["message"] = "Produsul a fost sters!";
            return RedirectToAction("Index");
        }
    }
}