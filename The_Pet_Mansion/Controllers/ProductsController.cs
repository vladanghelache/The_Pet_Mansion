using Microsoft.AspNet.Identity;
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
        //[Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Index()
        {
            var products = db.Products.Include("Category").Include("Animal").Include("User");
         
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

       

        [Authorize(Roles = "Editor,Admin")]
        public ActionResult New()
        {
            Product product = new Product();
            // preluam lista de categorii din metoda GetAllCategories()
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();
            //product.Vis = VisibleOptions();
            product.UserId = User.Identity.GetUserId();
            return View(product);
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpPost]
        public ActionResult New(Product product)
        {
            product.Date = DateTime.Now;
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();
           
            product.UserId = User.Identity.GetUserId();
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
                    product.Categories = GetAllCategories();
                    product.Animals = GetAllAnimals();
         
                    return View(product);
                }

            }
            catch (Exception e)
            {

                product.Categories = GetAllCategories();
                product.Animals = GetAllAnimals();
               
                return View(product);
            }
        }


        //[Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Show (int id)
        {
            Product product = db.Products.Find(id);
            var reviews = from var in db.Reviews
                          where var.ProductID == id
                          select var.Rating;


           if(reviews.Any())
            {
                product.AvgRating = reviews.Average();
            }
                
            
           
            db.SaveChanges();
            SetAccessRights();

            return View(product);
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpGet]
        public ActionResult Edit (int id)
        {
            Product product = db.Products.Find(id);
            product.Categories = GetAllCategories();
            product.Animals = GetAllAnimals();
            

            if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(product);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui produs care nu va apartine!";
                return RedirectToAction("Index");
            }

           

        }


        [Authorize(Roles = "Editor,Admin")]
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
                    if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                        if (TryUpdateModel(product))
                        {
                            product = requestProduct;
                            db.SaveChanges();
                            TempData["message"] = "Produsul a fost modificat cu succes!";
                            
                        }

                        return RedirectToAction("Index");

                    }

                    else
                    {
                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui produs care nu va apartine!";
                        return RedirectToAction("Index");
                    }

                    
                }

                else
                {
                    requestProduct.Categories = GetAllCategories();
                    requestProduct.Animals = GetAllAnimals();
                    return View(requestProduct);

                }
            }

            catch (Exception e)
            {
                requestProduct.Categories = GetAllCategories();
                requestProduct.Animals = GetAllAnimals();
                return View(requestProduct);
            }

            
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpDelete]
        public ActionResult Delete (int id)
        { 

           Product product = db.Products.Find(id);
            if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Products.Remove(product);
                db.SaveChanges();
                TempData["message"] = "Produsul a fost sters!";
                return RedirectToAction("Index");
            }

            else
            {
                
              TempData["message"] = "Nu aveti dreptul sa stergeti un produs care nu va apartine!";
              return RedirectToAction("Index");
                
            }
                
        }

        private void SetAccessRights()
        {
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Admin"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Admin");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
        }
    }
}