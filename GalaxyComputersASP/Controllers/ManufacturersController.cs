using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GalaxyComputersASP.Models;
using System.Data.Entity.Infrastructure;

namespace GalaxyComputersASP.Controllers
{
    public class ManufacturersController : Controller
    {
        private GalaxyComputersASPContext db = new GalaxyComputersASPContext();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturers.ToList());
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View(new Manufacturer { ImagePath = "/Images/placeholder.jpg" });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DirectedCreate(Manufacturer manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool exists = db.Manufacturers.FirstOrDefault(i => i.Name == manufacturer.Name) != null;
                    if (exists)
                    {
                        return Json(new { success = false, message = "Đã có danh mục với tên này trong cơ sở dữ liệu!" });
                    }
                    db.Manufacturers.Add(manufacturer);
                    db.SaveChanges();
                    return Json(new { success = true, name = manufacturer.Name, id = manufacturer.ID });
                }
            }
            catch (DbUpdateException)
            {
                return Json(new { success = false, message = "Dữ liệu nhập vào không hợp lệ!" });
            }

            return Json(new { success = false, message = "Dữ liệu nhập vào không hợp lệ!" });
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,WebPage,ImagePath,Country,DateFounded")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Manage", "Products");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,WebPage,ImagePath,Country,DateFounded")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage", "Products");
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Manage", "Products");
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
