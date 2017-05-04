using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class ArrangePickupViewModel
    {
        public virtual string Type { get; set; } // Immediate or Prearranged
        [Required]
        [StringLength(100)]
        public virtual string Address { get; set; }

        public virtual List<SelectListItem> Cities { get; set; }
        public virtual string City { get; set; }

        public virtual string Province { get; set; } // Autogenerate from City
        public virtual int PickupYear { get; set; }
        public virtual int PickupMonth { get; set; }
        public virtual int PickupDay { get; set; }
        public virtual int PickupHour { get; set; }
        public virtual int PickupMinute { get; set; }
    }
}