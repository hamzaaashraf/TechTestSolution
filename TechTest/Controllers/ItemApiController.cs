using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TechTest.Models;

namespace TechTest.Controllers
{
    public class ItemApiController : ApiController
    {
        private readonly ItemContext db = new ItemContext();
        public ItemApiController(ItemContext _db)
        {
            this.db = _db;
        }
        // GET: api/ItemApi
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetItems()
        {
            try
            {
                var items = db.Items.Where(i => i.IsActive).OrderByDescending(i => i.CreatedAt).ToList();
                if (items == null || !items.Any())
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                // Log the error (You can log to a file or logging service)
                LogError(ex);

                // Return a generic server error message
                return InternalServerError(new Exception("An error occurred while fetching items."));
            }
        }

        // GET: api/ItemApi/5
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            try
            {
                var item = db.Items.Find(id);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                // Log the error
                LogError(ex);

                // Return a generic server error message
                return InternalServerError(new Exception("An error occurred while fetching the item."));
            }
        }

        // POST: api/ItemApi
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult CreateItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Items.Add(item);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
            }
            catch (DbUpdateException dbEx)
            {
                // Log database update exception
                LogError(dbEx);

                // Return a specific error related to database update issues
                return Conflict();
            }
            catch (Exception ex)
            {
                // Log general exception
                LogError(ex);

                // Return a generic server error message
                return InternalServerError(new Exception("An error occurred while creating the item."));
            }
        }

        // PUT: api/ItemApi/5
        [System.Web.Http.HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult UpdateItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest("Item ID mismatch.");
            }

            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (Exception ex)
            {
                // Log error
                LogError(ex);

                return InternalServerError(new Exception("An error occurred while updating the item."));
            }
        }

        // DELETE: api/ItemApi/5
        [System.Web.Http.HttpDelete]
        [ValidateAntiForgeryToken]
        public IHttpActionResult DeleteItem(int id)
        {
            try
            {
                var item = db.Items.Find(id);
                if (item == null)
                {
                    return NotFound();
                }

                db.Items.Remove(item);
                db.SaveChanges();

                return Ok(item);
            }
            catch (Exception ex)
            {
                // Log error
                LogError(ex);

                return InternalServerError(new Exception("An error occurred while deleting the item."));
            }
        }

        // Check if an item exists
        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
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
            string logFilePath = "C:\\ErrorLogs\\api-errors.log";
            string message = $"{DateTime.Now}: {ex.Message} - {ex.StackTrace}";

            System.IO.File.AppendAllText(logFilePath, message + Environment.NewLine);
        }
    }
}
