using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class ShipmentsSearchViewModel
    {
        public virtual int ShippingAccountId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public virtual DateTime StartShippedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public virtual DateTime EndShippedDate { get; set; }
        public virtual List<SelectListItem> ShippingAccounts { get; set; }

        public ShipmentsSearchViewModel() {
            StartShippedDate = DateTime.Today;
            EndShippedDate = DateTime.Today;
        }
    }
}