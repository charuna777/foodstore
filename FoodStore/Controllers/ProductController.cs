﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodStore.DAL;
using FoodStore.Models;
using PagedList;

namespace FoodStore.Controllers
{
    public class ProductController : Controller
    {
        //Product[] products = new Product[]
        //{
        //    new Product { ID = 1, Name = "Tomato Kechup", Description = "Groceries", Price = 3.20M, Perishble=true, ExpiresOn=DateTime.Now.AddDays(20) },
        //    new Product { ID = 2, Name = "GreenPeas", Description = "Groceries", Price = 2.30M,Perishble=true, ExpiresOn=DateTime.Now.AddDays(60) },
        //    new Product { ID = 3, Name = "Sugar", Description = "Groceries", Price = 4, Perishble=false },
        //    new Product { ID = 4, Name = "Salt", Description = "Groceries", Price = 3,Perishble=false },
        //    new Product { ID = 5, Name = "Eggs", Description = "Groceries", Price = 6,Perishble=true,ExpiresOn=DateTime.Now.AddDays(30) }
        //};

        //public IEnumerable<Product> GetAllProducts()
        //{
        //      return products;
        //}

        private ProductContext db = new ProductContext();

        // GET: Product
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //var products = db.Products.ToList();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "ExpiresOn" ? "date_desc" : "ExpiresOn";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var products= from p in db.Products select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>p.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "ExpiresOn":
                    products = products.OrderBy(p => p.ExpiresOn);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(p => p.ExpiresOn);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumer = (page ?? 1);
            // return View(students.ToList());
            return View(products.ToPagedList(pageNumer, pageSize));

            //return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "unable to save changes, please check the code once.");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if(product==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productid_toupdate = db.Products.Find(id);
            if (TryUpdateModel(productid_toupdate))
            {
                try
                {
                    db.Entry(productid_toupdate).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes, please check the code");
                }
            }
            //return View(productid_toupdate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed, please try again.";
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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



