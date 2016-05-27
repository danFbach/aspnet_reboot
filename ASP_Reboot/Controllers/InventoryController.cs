using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ASP_Reboot.Models;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using System.Collections.Generic;

namespace ASP_Reboot.Controllers
{
    public class InventoryController : Controller
    {
        public string classpick(int quantity, string productName)
        {
            string a_class="";
            if(quantity >= 10)
            {
                a_class = "green";
            }
            else if (quantity >= 5)
            {
                a_class = "amber";
            }
            else if (quantity < 5)
            {
                warningMail(quantity.ToString(), productName);
                a_class = "red";
            }
            return a_class;
        }
        public async Task warningMail(string quantity, string productName)
        {
            var myMessage = new SendGridMessage();
            myMessage.From = new MailAddress("no-reply@devHax.prod", "MOTHA FUCKIN TINY RICK");
            myMessage.AddTo("theguy@wi.rr.com");
            myMessage.AddTo("charlesciezki@yahoo.com");
            myMessage.Subject = productName + " need to be refilled!";
            myMessage.Text = "There are only " + quantity + " " + productName + " remaining in stock. Order more soon.";
            var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inventory
        public ActionResult Index(int? searchString)
        {
            List<InventoryModels> inv = new List<InventoryModels>();

            if (searchString != (null))
            {
                var inventory = db.InventoryModels.ToList();
                foreach(InventoryModels item in inventory)
                {
                    if (item.SKU.Equals(searchString))
                    {
                        inv.Add(item);
                    }
                }
                return View(inv);
            }
            else if (searchString == null)
            {
                return View(db.InventoryModels.ToList());
            }
            return View(db.InventoryModels.ToList());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryModels inventoryModels = db.InventoryModels.Find(id);
            if (inventoryModels == null)
            {
                return HttpNotFound();
            }
            return View(inventoryModels);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SKU,productName,price,quantity,store")] InventoryModels inventoryModels)
        {
            if (ModelState.IsValid)
            {
                db.InventoryModels.Add(inventoryModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventoryModels);
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryModels inventoryModels = db.InventoryModels.Find(id);
            if (inventoryModels == null)
            {
                return HttpNotFound();
            }
            return View(inventoryModels);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SKU,productName,price,quantity,store")] InventoryModels inventoryModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventoryModels);
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryModels inventoryModels = db.InventoryModels.Find(id);
            if (inventoryModels == null)
            {
                return HttpNotFound();
            }
            return View(inventoryModels);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryModels inventoryModels = db.InventoryModels.Find(id);
            db.InventoryModels.Remove(inventoryModels);
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
