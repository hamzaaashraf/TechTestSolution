using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTest.Models;

namespace TechTest.Controllers
{
    public class ItemController : Controller
    {

        private readonly ItemContext db = new ItemContext();
        //public ItemController(ItemContext _db)
        //{
        //    this.db = _db;
        //}

        // GET: Item
        public ActionResult Index(string searchQuery)
        {
            try
            {
                var items = db.Items
                              .Where(i => i.IsActive); // Include only active items

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    items = items.Where(i => i.Name.Contains(searchQuery) || i.Description.Contains(searchQuery));
                }

                // Reapply ordering after filtering
                items = items.OrderByDescending(i => i.CreatedAt);
                if (items == null)
                {
                    TempData["ErrorMessage"] = "No items available!";
                    return View();
                }
                ViewBag.SearchQuery = searchQuery; // Pass the query back to the view
                return View(items.ToList());
            }
            catch (Exception ex)
            {
                // Log the error (you can log to a file or logging service)
                LogError(ex);

                // Display a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while fetching the items. Please try again.";
                return View(new List<Item>());
            }
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    TempData["Message"] = "Item created successfully!";
                    return RedirectToAction("Index");
                }

                // If validation fails, return the same view with the current model to display errors
                return View(item);
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while creating the item. Please try again.";
                return View(item);
            }
        }

        // GET: Item/Edit/{id}
        public ActionResult Edit(int id)
        {
            try
            {
                var item = db.Items.Find(id);
                if (item == null) return HttpNotFound();
                return View(item);
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while fetching the item details. Please try again.";
                return RedirectToAction("Index");
            }
        }

        // POST: Item/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Item updated successfully!";
                    return RedirectToAction("Index");
                }
                return View(item);
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while updating the item. Please try again.";
                return View(item);
            }
        }

        // GET: Item/Delete/{id}
        public ActionResult Delete(int id)
        {
            try
            {
                var item = db.Items.Find(id);
                if (item == null) return HttpNotFound();
                return View(item);
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while fetching the item details for deletion. Please try again.";
                return RedirectToAction("Index");
            }
        }

        // POST: Item/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var item = db.Items.Find(id);
                if (item == null) return HttpNotFound();

                db.Items.Remove(item);
                db.SaveChanges();
                TempData["Message"] = "Item deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a user-friendly error message
                TempData["ErrorMessage"] = "An error occurred while deleting the item. Please try again.";
                return RedirectToAction("Index");
            }
        }

        // Dispose the context to free resources
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Custom method to log errors
        private void LogError(Exception ex)
        {
            // You can log errors to a file or logging service (e.g., Log4Net, Serilog, etc.)
            // Example: Write errors to a log file
            string logFilePath = "C:\\ErrorLogs\\controller-errors.log";
            string message = $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}";

            System.IO.File.AppendAllText(logFilePath, message + Environment.NewLine);
        }
    }
}