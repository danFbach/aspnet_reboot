using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ASP_Reboot.Models
{
    public class StoreInvViewModel
    {
        public int inventory_Id { get; set; }
        public int SKU { get; set; }
        [Display(Name = "Product Name")]
        public string productName { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Quantity In Stock")]
        public int quantity { get; set; }
        [Display(Name = "Store")]
        public int inv_store_id { get; set; }
        public int store_Id { get; set; }
        public string city { get; set; }
        public double getLong { get; set; }
        public double geoLat { get; set; }
        public List<SelectListItem> alist { get; set; }
    }
}