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
        private ApplicationDbContext db = new ApplicationDbContext();

        //public object locations(List<int> Id)
        //{
        //    var currentLocations = "";
        //    foreach(int id in Id)
        //    {
        //        currentLocations += (db.StoreModels.Find(id));
        //    }
        //    return currentLocations;
        //}
        public string classpick(int quantity, string productName, int id)
        {
            string a_class="";
            InventoryModels current_product = db.InventoryModels.Find(id);

            if (quantity >= 10)
            {
                current_product.warningSent = 1;
                a_class = "green";
            }
            else if (quantity >= 5)
            {
                current_product.warningSent = 1;
                a_class = "amber";
            }
            else if (quantity < 5)
            {
                if (current_product.warningSent.Equals(1))
                {
                    warningMail(quantity.ToString(), productName);
                    current_product.warningSent = 0;
                }
                a_class = "red";
            }
            db.SaveChanges();
            return a_class;
        }
        public async Task warningMail(string quantity, string productName)
        {
            var myMessage = new SendGridMessage();
            myMessage.From = new MailAddress("no-reply@devHax.prod", "MOTHA FUCKIN TINY RICK");
            myMessage.AddTo("theguy@wi.rr.com");
            myMessage.Subject = productName + " need to be refilled!";
            myMessage.Text = "There are only " + quantity + " " + productName + " remaining in stock. Order more soon.";
            var credentials = new NetworkCredential("quikdevstudent", "Lexusi$3");
            var transportWeb = new Web(credentials);
            await transportWeb.DeliverAsync(myMessage);
        }

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
        public ActionResult Create([Bind(Include = "Id,SKU,productName,price,quantity,warningSent,store_id")] InventoryModels inventoryModels)
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
        public ActionResult Edit([Bind(Include = "Id,SKU,productName,price,quantity,warningSent,store_id")] InventoryModels inventoryModels)
        {
            var warn = inventoryModels.warningSent;
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
        public List<SelectListItem> getStoreDropDown()
        {
            List<SelectListItem> storelist = new List<SelectListItem>();
            var stores = db.StoreModels.ToList();
            foreach (StoreModels store in stores)
            {
                string txt = "Id: " + store.Id + " City: " + store.city;
                storelist.Add(new SelectListItem() { Text = txt, Value = store.Id.ToString() });
            }
            return storelist;
        }
    }
}
