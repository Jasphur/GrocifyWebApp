using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrocifyAppMVC.Models;

namespace GrocifyAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult ChangeBoughtStatus()
        {
            Product product = new Product();

            if (product.Status != Status.Bought)
            {
                product.Status = Status.Bought;
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
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
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Name = User.Identity.Name;
                db.Products.Add(product);
                // Genereert 1 keer een file en voegt elke keer een melding toe
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "GrocifyVolledigeLog.txt"), true))
                {
                    outputFile.WriteLine($"Je hebt {product.Amount} {(product.ProductName).ToLower()} aan je lijst toegevoegd, met de status {product.Status}" + " " + DateTime.Now);
                }

                // Genereert elke keer 1 file met 1 melding
                using (StreamWriter writer = new StreamWriter(Path.Combine(mydocpath, "GrocifyNieuweLogToevoeging.txt")))
                {
                    writer.WriteLine($"Je hebt {product.Amount} {(product.ProductName).ToLower()} aan je lijst toegevoegd" + " " + DateTime.Now);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                //product.Name = product.Name;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View(product);
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
