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
        public ActionResult Index()
        {
            return View(db.Products.ToList());
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
                list.Add(new ProductOverview { Product = product, Category = category, Manufacturer = manufacturer });
            }
            return View(list.ToList());
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

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Description,CategoryID,Brand,ImagePath")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.PublishDate = DateTime.Now;
                db.Products.Add(product);
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PublishDate,Name,Price,Description,Category,Brand,ImagePath,Views,Sales")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return View(new ProductOverview { Product = product, Manufacturer = manufacturer, Category = category });
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
