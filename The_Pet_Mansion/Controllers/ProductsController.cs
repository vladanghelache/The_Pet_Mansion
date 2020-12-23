using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int _perPage = 3;

        // GET: Products
        //[Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Index()
        {
            //var sort = "";
            var products = db.Products.Include("Category").Include("Animal").Include("User").Include("File").OrderBy(a => a.Price);

                 if (Request.Params.Get("Sort") == "2")
                {
                    products = db.Products.Include("Category").Include("Animal").Include("User").Include("File").OrderByDescending(a => a.Price);
                }

            else
                if (Request.Params.Get("Sort") == "3")
            {
                products = db.Products.Include("Category").Include("Animal").Include("User").Include("File").OrderBy(a => a.AvgRating);
            }
                 else
                 if (Request.Params.Get("Sort") == "4")
            {
                products = db.Products.Include("Category").Include("Animal").Include("User").Include("File").OrderByDescending(a => a.AvgRating);
            }




                /*var files = from path in db.UploadFiles
                            where 
                            select path;            ViewBag.Files = files;
                */
                var search = "";
            if(Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim();
                List<int> productsIds = db.Products.Where(
                    pr => pr.ProductName.Contains(search)
                    ).Select(p => p.ProductID).ToList();
                products = db.Products.Where(product => productsIds.Contains(product.ProductID)).Include("Category").Include("Animal").Include("User").Include("File").OrderBy(a => a.Price);
                if (Request.Params.Get("Sort") == "2")
                {
                    products = db.Products.Where(product => productsIds.Contains(product.ProductID)).Include("Category").Include("Animal").Include("User").Include("File").OrderByDescending(a => a.Price);
                }

                else
               if (Request.Params.Get("Sort") == "3")
                {
                    products = db.Products.Where(product => productsIds.Contains(product.ProductID)).Include("Category").Include("Animal").Include("User").Include("File").OrderBy(a => a.AvgRating);
                }
                else
                if (Request.Params.Get("Sort") == "4")
                {
                    products = db.Products.Where(product => productsIds.Contains(product.ProductID)).Include("Category").Include("Animal").Include("User").Include("File").OrderByDescending(a => a.AvgRating);
                }
            }
           
            var totalItems = products.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));
            var offset = 0;
            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }
            var paginatedProducts = products.Skip(offset).Take(this._perPage);
           
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            ViewBag.perPage = this._perPage;
            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Products = paginatedProducts;
            ViewBag.SearchString = search;
            ViewBag.sort = Request.Params.Get("Sort");
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
            Class1 c = new Class1();
            // preluam lista de categorii din metoda GetAllCategories()
            c.Product.Categories = GetAllCategories();
            c.Product.Animals = GetAllAnimals();
            //product.Vis = VisibleOptions();
           c.Product.UserId = User.Identity.GetUserId();
            return View(c);
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpPost]
        public ActionResult New(Class1 c)
        {
            c.Product.Date = DateTime.Now;
            c.Product.Categories = GetAllCategories();
            c.Product.Animals = GetAllAnimals();
           
            c.Product.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(c.Product);
                    db.SaveChanges();
                    TempData["message"] = "Produsul a fost adaugat cu succes!";
                    Upload2(c.UploadedFile, c.Product.ProductID);
                    return Redirect("Index");                   
                }

                else
                {
                    c.Product.Categories = GetAllCategories();
                    c.Product.Animals = GetAllAnimals();
         
                    return View(c);
                }

            }
            catch (Exception e)
            {

                c.Product.Categories = GetAllCategories();
                c.Product.Animals = GetAllAnimals();
               
                return View(c);
            }
        }

        [NonAction]
        public ActionResult Upload2(HttpPostedFileBase uploadedFile, int id)
        {
            // Se preia numele fisierul
            string uploadedFileName = uploadedFile.FileName;
            string uploadedFileExtension = Path.GetExtension(uploadedFileName);

            // Se poate verifica daca extensia este intr-o lista dorita
            if (uploadedFileExtension == ".png" || uploadedFileExtension == ".jpg" || uploadedFileExtension == ".pdf")
            {
                // Se stocheaza fisierul in folderul Files (folderul trebuie creat in proiect)

                // 1. Se seteaza calea folderului de upload
                string uploadFolderPath = Server.MapPath("~//Files//");

                // 2. Se salveaza fisierul in acel folder
                uploadedFile.SaveAs(uploadFolderPath + uploadedFileName);

                // 3. Se face o instanta de model si se populeaza cu datele necesare
                UploadFile file = new UploadFile();
                file.FileId = id;
                file.Extension = uploadedFileExtension;
                file.FileName = uploadedFileName;
                file.FilePath = uploadFolderPath + uploadedFileName;

                // 4. Se adauga modelul in baza de date
                db.UploadFiles.Add(file);
                db.SaveChanges();

                // 5. Return catre index cu mesaj de succes - TODO
                return Redirect("/Products/Index");

            }

            // TODO: tratarea erorilor
            return View();
        }

        [NonAction]
        public ActionResult ChangePhoto(HttpPostedFileBase uploadedFile, int id)
        {
            // Se preia numele fisierul
            string uploadedFileName = uploadedFile.FileName;
            string uploadedFileExtension = Path.GetExtension(uploadedFileName);

            // Se poate verifica daca extensia este intr-o lista dorita
            if (uploadedFileExtension == ".png" || uploadedFileExtension == ".jpg" || uploadedFileExtension == ".pdf")
            {
                // Se stocheaza fisierul in folderul Files (folderul trebuie creat in proiect)

                // 1. Se seteaza calea folderului de upload
                string uploadFolderPath = Server.MapPath("~//Files//");

                // 2. Se salveaza fisierul in acel folder
                uploadedFile.SaveAs(uploadFolderPath + uploadedFileName);

                
                UploadFile file = db.UploadFiles.Find(id);
                
                file.Extension = uploadedFileExtension;
                file.FileName = uploadedFileName;
                file.FilePath = uploadFolderPath + uploadedFileName;

                
                db.SaveChanges();

                
                return Redirect("/Products/Index");

            }

            // TODO: tratarea erorilor
            return View();
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

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View(product);
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpGet]
        public ActionResult Edit (int id)
        {
            Class1 c = new Class1();
            c.Product = db.Products.Find(id);
            c.Product.Categories = GetAllCategories();
            c.Product.Animals = GetAllAnimals();
            

            if (c.Product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(c);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui produs care nu va apartine!";
                return RedirectToAction("Index");
            }

           

        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpPut]
        public ActionResult Edit (int id, Class1 c)
        {

            c.Product.Categories = GetAllCategories();
            c.Product.Animals = GetAllAnimals();
           
            try
            {
                if (ModelState.IsValid)
                {
                   
                    Product product = db.Products.Find(id);
                    if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                       
                        if (TryUpdateModel(product))
                        {
                            
                            product.AnimalID = c.Product.AnimalID;
                            product.CategoryID = c.Product.CategoryID;
                            product.Description = c.Product.Description;
                            product.Price = c.Product.Price;
                            product.Visible = c.Product.Visible;
                            product.Date = c.Product.Date;
                            product.ProductName = c.Product.ProductName;
                            product.Stock = c.Product.Stock;
                            
                            db.SaveChanges();
                           
                            TempData["message"] = "Produsul a fost modificat cu succes!";
                            
                            if (c.UploadedFile != null)
                            {
                                ChangePhoto(c.UploadedFile, id);
                            }

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
                    c.Product.Categories = GetAllCategories();
                    c.Product.Animals = GetAllAnimals();
                    return View(c);

                }
            }

            catch (Exception e)
            {
                c.Product.Categories = GetAllCategories();
                c.Product.Animals = GetAllAnimals();
                return View(c);
            }

            
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpDelete]
        public ActionResult Delete (int id)
        { 

            Product product = db.Products.Find(id);
            UploadFile file = db.UploadFiles.Find(id);
            if (product.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Products.Remove(product);
                db.UploadFiles.Remove(file);
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