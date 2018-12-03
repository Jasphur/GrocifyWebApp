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
using PagedList;

namespace GrocifyAppMVC.Controllers
{
	[Authorize]
	public class MyListListController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: MyListList
		public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";
			ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
			ViewBag.BoughtBySortParm = sortOrder == "BoughtBy" ? "boughtBy_desc" : "BoughtBy";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;
			
			IEnumerable<MyListModel> Model = db
				.Products
				.Where(s => s.Name == User.Identity.Name && s.HiddenStatus != HiddenStatus.Archived)
				.Select(s => new MyListModel
				{
					Id = s.Id,
					ProductName = s.ProductName,
					Amount = s.Amount,
					Name = s.Name,
					Status = s.Status,
					BoughtBy = s.BoughtBy
				});

			if (!String.IsNullOrEmpty(searchString))
			{
				Model = Model.Where(s => s.ProductName.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					Model = Model.OrderByDescending(s => s.ProductName);
					break;
				case "Amount":
					Model = Model.OrderBy(s => s.Amount);
					break;
				case "amount_desc":
					Model = Model.OrderByDescending(s => s.Amount);
					break;
				case "Status":
					Model = Model.OrderBy(s => s.Status);
					break;
				case "status_desc":
					Model = Model.OrderByDescending(s => s.Status);
					break;
				case "BoughtBy":
					Model = Model.OrderBy(s => s.BoughtBy);
					break;
				case "boughtBy_desc":
					Model = Model.OrderByDescending(s => s.BoughtBy);
					break;
				default:
					Model = Model.OrderBy(s => s.ProductName);
					break;
			}

			int pageSize = 10;
			int pageNumber = (page ?? 1);

			return View(Model.ToPagedList(pageNumber, pageSize));
		}

		// GET: MyListList/Details/5
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

		// GET: MyListList/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MyListList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.

		string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] Product product)
		{
			// Archived enum uit de dropdown halen
			//var selectList = Enum.GetValues(typeof(Status))
			//           .Cast<Status>()
			//           .Where(e => e != Status.Archived)
			//           .Select(e => new SelectListItem
			//           {
			//               Value = ((int)e).ToString(),
			//               Text = e.ToString()
			//           });

			if (ModelState.IsValid)
			{
				product.Name = User.Identity.Name;
				db.Products.Add(product);

				// Genereert 1 keer een file en voegt elke keer een melding toe
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "GrocifyVolledigeLog.txt"), true))
				{
					outputFile.WriteLine($"Je hebt {product.Amount} stuk(s) {(product.ProductName).ToLower()} aan je lijst toegevoegd, met de status \"{product.Status.GetDisplayName().ToLower()}\"" + " " + DateTime.Now);
				}

				// Genereert elke keer 1 file met 1 melding
				using (StreamWriter writer = new StreamWriter(Path.Combine(mydocpath, "GrocifyNieuweLogToevoeging.txt")))
				{
					writer.WriteLine($"Je hebt {product.Amount} stuk(s) {(product.ProductName).ToLower()} aan je lijst toegevoegd" + " " + DateTime.Now);
				}

				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: MyListList/Edit/5
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

		// POST: MyListList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] Product product)
		{
			var OldProductAmount = product.Amount;
			var OldProductName = product.ProductName;
			var OldProductStatus = product.Status;

			if (ModelState.IsValid)
			{
				product.Name = User.Identity.Name;
				db.Entry(product).State = EntityState.Modified;

				// Genereert 1 keer een file en voegt elke keer een melding toe
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "GrocifyVolledigeLog.txt"), true))
				{
					//outputFile.WriteLine($"Je hebt {OldProductName.ToLower()} gewijzigd" +
					//                     $"\r\n{OldProductAmount} stuk(s) gewijzigd naar {product.Amount}" +
					//                     $"\r\n{OldProductName.ToLower()} gewijzigd naar {product.ProductName.ToLower()}" +
					//                     $"\r\nen status \"{OldProductStatus.GetDisplayName().ToLower()}\" gewijzigd naar {product.Status.GetDisplayName().ToLower()}" + " op " + DateTime.Now);

					outputFile.WriteLine("");
					outputFile.WriteLine($"Je hebt {product.ProductName.ToLower()} gewijzigd  op " + DateTime.Now +
											 $"\r\nAantal stuk(s) gewijzigd naar \"{product.Amount}\"" +
											 $"\r\nProductnaam gewijzigd naar \"{product.ProductName}\"" +
											 $"\r\nen de status gewijzigd naar \"{product.Status.GetDisplayName()}\"");

				}

				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(product);
		}

		// GET: MyListList/Delete/5
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

		// POST: MyListList/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = db.Products.Find(id);
			product.Status = (Status)5;
			product.HiddenStatus = HiddenStatus.Archived;
			//db.Products.Remove(product);

			// Genereert 1 keer een file en voegt elke keer een melding toe
			using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "GrocifyVolledigeLog.txt"), true))
			{
				outputFile.WriteLine($"Je hebt {product.Amount} stuk(s) {(product.ProductName).ToLower()} uit je lijst verwijderd" + " " + DateTime.Now);
			}

			// Genereert elke keer 1 file met 1 melding
			using (StreamWriter writer = new StreamWriter(Path.Combine(mydocpath, "GrocifyNieuweLogToevoeging.txt")))
			{
				writer.WriteLine($"Je hebt {product.Amount} stuk(s) {(product.ProductName).ToLower()} uit je lijst verwijderd" + " " + DateTime.Now);
			}

			var Products = db.Products;
			var HistoryProducts = db.HistoryProductsModels;

			//Products.AsEnumerable()
			//    .Where(s => s.HiddenStatus == HiddenStatus.Archived)
			//    .CopyToDataTable(HistoryProducts, LoadOption.OverwriteChanges);

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
