using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GrocifyAppMVC.Models;
using System.IO;
using PagedList;
using System.Windows.Forms;
using System.Net.Mail;
using System.Web.Security;

namespace GrocifyAppMVC.Controllers
{
	[Authorize]
	public class CollectiveListController : Controller
	{

		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult UserNameDrop()
		{
			var items = db.Users.ToList();
			if (items != null)
			{
				ViewBag.UserNameDrop = items;
			}
			return View();
		}

        [HttpPost]
        public ActionResult ChangeBoughtStatus(string button)
        {
            Product product = new Product();

            product.Status = Status.Bought;
            return RedirectToAction("Index");

        }

		// GET: CollectiveList

		public ViewResult UserNames()
		{
			ViewBag.UserNameDrop = new SelectList(db.Users.ToList(), "Id", "UserName");
			return View();
		}


		public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Productname_desc" : "";
			ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";
			ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;


			IEnumerable<CollectiveListModel> Model = db
				.Products
				.Where(s => s.Status == Status.GetItForMe)
				.Select(s => new CollectiveListModel
				{
					Id = s.Id,
					ProductName = s.ProductName,
					Amount = s.Amount,
					Name = s.Name,
					BoughtBy = s.BoughtBy
				});
		


			if (!String.IsNullOrEmpty(searchString))
			{
				Model = Model.Where(s => s.ProductName.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "Productname_desc":
					Model = Model.OrderByDescending(s => s.ProductName);
					break;
				case "Amount":
					Model = Model.OrderBy(s => s.Amount);
					break;
				case "amount_desc":
					Model = Model.OrderByDescending(s => s.Amount);
					break;
				case "Name":
					Model = Model.OrderBy(s => s.Name);
					break;
				case "name_desc":
					Model = Model.OrderByDescending(s => s.Name);
					break;
				default:
					Model = Model.OrderBy(s => s.ProductName);
					break;
			}

			int pageSize = 10;
			int pageNumber = (page ?? 1);

			ViewBag.OnePageOfUsers = searchString;

			return View(Model.ToPagedList(pageNumber, pageSize));
		}



		// GET: CollectiveList/Details/5
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

		// GET: CollectiveList/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CollectiveList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy")] Product product)
		{
			if (ModelState.IsValid)
			{
				db.Products.Add(product);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: CollectiveList/Edit/5
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

		// POST: CollectiveList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.

		string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,ProductName,Amount,Status,Name,BoughtBy,Debt")] Product product)
		{
			if (ModelState.IsValid)
			{
				product.ProductName = product.ProductName;
				product.Amount = product.Amount;
				product.Name = product.Name;
				product.BoughtBy = User.Identity.Name;
				db.Entry(product).State = EntityState.Modified;

				if (product.Status == Status.Bought)
				{

					if (product.Status == Status.Bought)
					{
						// Genereert 1 keer een file en voegt elke keer een melding toe
						using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "GrocifyLogMelding.txt"), true))
						{
							outputFile.WriteLine($"{product.BoughtBy} heeft {(product.ProductName).ToLower()} voor je ({product.Name}) gekocht!" + " " + DateTime.Now);
						}

						// Genereert elke keer 1 file met 1 melding
						using (StreamWriter writer = new StreamWriter(Path.Combine(mydocpath, "GrocifyMelding.txt")))
						{
							writer.WriteLine($"{product.BoughtBy} heeft {(product.ProductName).ToLower()} voor je ({product.Name}) gekocht!" + " " + DateTime.Now);
						}
					}

					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
					MessageBox.Show($"Zet de status op {Status.Bought.GetDisplayName().ToLower()}");
			}
			return View(product);
		}

		// GET: CollectiveList/Delete/5
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

		// POST: CollectiveList/Delete/5
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
