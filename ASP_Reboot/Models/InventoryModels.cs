using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_Reboot.Models
{
    public class InventoryModels {

        [Required]
        public virtual int Id { get; set; }

        [Required]
        [Display(Name ="SKU")]
        public virtual int SKU { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public virtual string productName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public virtual decimal price { get; set; }

        [Display(Name = "Quantity")]
        public virtual int quantity { get; set; }
    }
}