﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_Reboot.Models
{
    public class InventoryModels {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="SKU")]
        public int SKU { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        [Display(Name ="warningSent")]
        public int warningSent { get; set; }

        [Required]
        [Display(Name = "Store Id")]
        public int store_id { get; set; }

        [Required]
        [Display(Name = "Warning Level")]
        public int warningLevel { get; set; }

        [Required]
        [Display(Name = "Refill Level")]
        public int refillLevel { get; set; }

    }
}