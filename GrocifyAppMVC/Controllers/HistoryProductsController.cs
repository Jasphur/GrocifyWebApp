using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrocifyAppMVC.Models;

namespace GrocifyAppMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class HistoryProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HistoryProducts
        public ActionResult Index()
        {
            return View(db.HistoryProductsModels.ToList());
        }

        // GET: HistoryProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryProductsModel historyProductsModel = db.HistoryProductsModels.Find(id);
            if (historyProductsModel == null)
            {
                return HttpNotFound();
            }
            return View(historyProductsModel);
        }

        // GET: HistoryProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoryProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] HistoryProductsModel historyProductsModel)
        {
            if (ModelState.IsValid)
            {
                db.HistoryProductsModels.Add(historyProductsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historyProductsModel);
        }

        // GET: HistoryProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryProductsModel historyProductsModel = db.HistoryProductsModels.Find(id);
            if (historyProductsModel == null)
            {
                return HttpNotFound();
            }
            return View(historyProductsModel);
        }

        // POST: HistoryProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] HistoryProductsModel historyProductsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historyProductsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historyProductsModel);
        }

        // GET: HistoryProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryProductsModel historyProductsModel = db.HistoryProductsModels.Find(id);
            if (historyProductsModel == null)
            {
                return HttpNotFound();
            }
            return View(historyProductsModel);
        }

        // POST: HistoryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoryProductsModel historyProductsModel = db.HistoryProductsModels.Find(id);
            db.HistoryProductsModels.Remove(historyProductsModel);
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
