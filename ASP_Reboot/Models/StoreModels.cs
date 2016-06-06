using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Reboot.Models
{
    public class StoreModels
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "City")]
        public virtual string city { get; set; }

        [Required]
        [Display(Name = "Store Address")]
        public virtual string address { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public virtual int zipcode { get; set; }

        [Required]
        [Display(Name = "GeoLat")]
        public virtual double geoLat { get; set; }

        [Required]
        [Display(Name = "GeoLong")]
        public virtual double getLong { get; set; }
        
    }
}