using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21487822_HW06.Models;
using PagedList;

namespace u21487822_HW06.Controllers
{
    public class productsController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();


        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = db.products.Include(p => p.brand).Include(p => p.category);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.product_name.Contains(searchString));
            }

            products = products.OrderBy(x => x.product_id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ReturnViewBagsWithParams();
            return PartialView(nameof(Create));
        }

        [HttpPost]
        public ActionResult Create(product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return Json(new { RetVal = true });
            }

            ReturnViewBagsWithParams(product);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ReturnViewBagsWithParams(product);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { RetVal = true });
            }

            return PartialView(nameof(Edit), new { id = product.product_id });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(nameof(Delete), product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return Json(new { RetVal = true });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(nameof(Details), product);
        }

        private void ReturnViewBagsWithParams(product product = null)
        {
            if (product == null)
            {
                ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
                ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            }
            else
            {
                ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
                ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            }
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
