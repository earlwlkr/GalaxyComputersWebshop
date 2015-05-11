using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GalaxyComputersASP.Models;

namespace GalaxyComputersASP.Controllers
{
    public class ProductsController : Controller
    {
        private GalaxyComputersASPContext db = new GalaxyComputersASPContext();

        // GET: Products
        public ActionResult Index(int? CategoryID)
        {
            if (CategoryID.HasValue)
            {
                List<Product> list = db.Products.Where(i => i.CategoryID == CategoryID).ToList();
                return View(list);
            }
            return View(db.Products.ToList());
        }

        // GET: Products
        [HttpPost]
        public ActionResult PartialIndex(int? CategoryID, FormCollection collection)
        {
            DbSet<Product> list = db.Products;
            
            int sortItem = int.Parse(collection["sortItem"]);
            int sortOrder = int.Parse(collection["sortOrder"]);

            IQueryable<Product> orderedList;
            if (sortItem == 0)
            {
                if (sortOrder == 0)
                {
                    orderedList = list.OrderBy(i => i.Name);
                    if (CategoryID.HasValue)
                    {
                        orderedList = orderedList.Where(i => i.CategoryID == CategoryID);
                    }
                }
                else
                {
                    orderedList = list.OrderByDescending(i => i.Name);
                    if (CategoryID.HasValue)
                    {
                        orderedList = orderedList.Where(i => i.CategoryID == CategoryID);
                    }
                }
            }
            else
            {
                if (sortOrder == 0)
                {
                    orderedList = list.OrderBy(i => i.Price);
                    if (CategoryID.HasValue)
                    {
                        orderedList = orderedList.Where(i => i.CategoryID == CategoryID);
                    }
                }
                else
                {
                    orderedList = list.OrderByDescending(i => i.Price);
                    if (CategoryID.HasValue)
                    {
                        orderedList = orderedList.Where(i => i.CategoryID == CategoryID);
                    }
                }
            }
            return PartialView(orderedList.ToList());
        }

        // GET: Products/Manage
        public ActionResult Manage()
        {
            IEnumerable<Product> products = db.Products.ToList();
            List<ProductOverview> list = new List<ProductOverview>();
            foreach (Product product in products)
            {
                Category category = db.Categories.Find(product.CategoryID);
                Manufacturer manufacturer = db.Manufacturers.Find(product.ManufacturerID);
                list.Add(new ProductOverview { ProductData = product, ProductCategory = category, ProductManufacturer = manufacturer });
            }
            return View(new ProductManageViewModel { Products = list.ToList(), Categories = db.Categories.ToList(),
                                                     Manufacturers = db.Manufacturers.ToList() });
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(product.CategoryID);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(new ProductDetailsViewModel { Product = product, Category = category });
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            this.ViewBag.CategoriesList = GetCategoriesList();
            this.ViewBag.ManufacturersList = GetManufacturersList();

            return View(new ProductOverview { ProductData = new Product { PublishDate = DateTime.Now, ImagePath = "/Images/placeholder.jpg" }, 
                    ProductManufacturer = new Manufacturer { ImagePath = "/Images/placeholder.jpg" } });
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductOverview product)
        {
            if (ModelState.IsValid)
            {
                product.ProductData.PublishDate = DateTime.Now;
                db.Products.Add(product.ProductData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.ViewBag.CategoriesList = GetCategoriesList();
            this.ViewBag.ManufacturersList = GetManufacturersList();

            return View(product);
        }

        private SelectList GetCategoriesList()
        {
            List<Category> categories = db.Categories.ToList();
            var categoriesList = new List<SelectListItem>();
            foreach (Category category in categories)
            {
                categoriesList.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
            }
            return new SelectList(categoriesList, "Value", "Text", categories[0].Name);
        }

        private SelectList GetManufacturersList()
        {
            List<Manufacturer> manufacturers = db.Manufacturers.ToList();
            var manufacturersList = new List<SelectListItem>();
            foreach (Manufacturer manufacturer in manufacturers)
            {
                manufacturersList.Add(new SelectListItem { Text = manufacturer.Name, Value = manufacturer.ID.ToString() });
            }
            return new SelectList(manufacturersList, "Value", "Text", manufacturers[0].Name);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            var httpPostedFile = Request.Files["UploadedImage"];
            if (httpPostedFile != null)
            {
                string path = @"~\Images\" + httpPostedFile.FileName;
                string returnPath = @"/Images/" + httpPostedFile.FileName;
                httpPostedFile.SaveAs(Server.MapPath(path));

                return Json(new { success = true, path = returnPath });
            }
            return Json(new { success = false, message = "Lỗi khi upload file." });
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            this.ViewBag.CategoriesList = GetCategoriesList();
            this.ViewBag.ManufacturersList = GetManufacturersList();
            return View(new ProductOverview { ProductData = product, ProductManufacturer = new Manufacturer { ImagePath = "/Images/placeholder.jpg" } });
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductOverview product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product.ProductData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            this.ViewBag.CategoriesList = GetCategoriesList();
            this.ViewBag.ManufacturersList = GetManufacturersList();
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(product.CategoryID);
            Manufacturer manufacturer = db.Manufacturers.Find(product.ManufacturerID);
            return View(new ProductOverview { ProductData = product, ProductManufacturer = manufacturer, ProductCategory = category });
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
