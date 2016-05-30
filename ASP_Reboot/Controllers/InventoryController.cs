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
            var inventory = db.InventoryModels.ToList();

            if (searchString != (null))
            {
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
                foreach(InventoryModels items in inventory)
                {
                    if(items.quantity < 5 && items.warningSent == 0)
                    {
                        warningMail(items.quantity.ToString(), items.productName);
                        items.warningSent = 1;
                    }
                    else if(items.quantity >= 5)
                    {
                        items.warningSent = 0;
                    }
                    db.SaveChanges();
                }
                return View(inventory);
            }
            return View(inventory);
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
            List<InventoryModels> inv = db.InventoryModels.ToList();
            List<StoreModels> stores = db.StoreModels.ToList();
            
            for (int i = 0; i < inv.Count(); i++)
            {
                InventoryModels inventory = inv[i];

            }
            List<SelectListItem> storelist = new List<SelectListItem>();
            StoreInvViewModel storemodel = new StoreInvViewModel();
            
            foreach (StoreModels store in stores)
            {
                
                string txt = "Id: " + store.Id + " City: " + store.city;
                storelist.Add(new SelectListItem() { Text = txt, Value = store.Id.ToString() });
            }
            storemodel.alist = storelist;

            return View(storemodel);
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreInvViewModel storeinv)
        {
            InventoryModels inv = new InventoryModels();

            if (ModelState.IsValid)
            {
                inv.Id = storeinv.inventory_Id;
                inv.price = storeinv.price;
                inv.productName = storeinv.productName;
                inv.quantity = storeinv.quantity;
                inv.SKU = storeinv.SKU;
                inv.warningSent = 0;
                inv.store_id = storeinv.inv_store_id;


                db.InventoryModels.Add(inv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeinv);
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {

            InventoryModels inv = db.InventoryModels.Find(id);
            List<StoreModels> stores = db.StoreModels.ToList();
            List<SelectListItem> storelist = new List<SelectListItem>();
            StoreInvViewModel storeinv = new StoreInvViewModel();

            
            foreach (StoreModels store in stores)
            {

                string txt = "Id: " + store.Id + " City: " + store.city;
                storelist.Add(new SelectListItem() { Text = txt, Value = store.Id.ToString() });
            }

            storeinv.inventory_Id = inv.Id;
            storeinv.SKU = inv.SKU;
            storeinv.productName = inv.productName;
            storeinv.price = inv.price;
            storeinv.quantity = inv.quantity;
            storeinv.inv_store_id = inv.store_id;
            storeinv.alist = storelist;
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryModels inventoryModels = db.InventoryModels.Find(id);
            if (inventoryModels == null)
            {
                return HttpNotFound();
            }
            return View(storeinv);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreInvViewModel sivm)
        {

            InventoryModels invEdit = db.InventoryModels.Find(sivm.inventory_Id);
            db.InventoryModels.Remove(invEdit);
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                invEdit.Id = sivm.inventory_Id;
                invEdit.price = sivm.price;
                invEdit.productName = sivm.productName;
                invEdit.quantity = sivm.quantity;
                invEdit.SKU = sivm.SKU;
                invEdit.warningSent = 0;
                invEdit.store_id = sivm.inv_store_id;

                db.InventoryModels.Add(invEdit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sivm);
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
