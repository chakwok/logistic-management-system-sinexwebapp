using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20265462.Models;

namespace SinExWebApp20265462.ViewModels
{
    public class ArrangeShipmentViewModel
    {
        [StringLength(5, ErrorMessage = "Reference Number cannot exceed 5 characters.")]
        public virtual string ReferenceNumber { get; set; }

        public virtual int ServiceTypeID { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual List<SelectListItem> ServiceTypes { get; set; }

        public virtual int RecipientAddressID { get; set; }
        public virtual List<SelectListItem> RecipientAddresses { get; set; }

        [Required]
        [StringLength(70)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public virtual string RecipientName { get; set; }
        [RegularExpression("[0-9]{12,12}", ErrorMessage = "Please enter a 12-digit number.")]
        public virtual string RecipientShippingAccountId { get; set; } // If Payer is Recipient
        [StringLength(40)]
        public virtual string RecipientCompanyName { get; set; } // If any
        [StringLength(30)]
        public virtual string RecipientDepartmentName { get; set; } // If any
        [StringLength(50)]
        public virtual string RecipientBuilding { get; set; } // If any
        [Required]
        [StringLength(35)]
        public virtual string RecipientStreet { get; set; }

        public virtual List<SelectListItem> Destinations { get; set; } // For populating destination list
        public virtual string Destination { get; set; } // Recipient's City

        public virtual string RecipientProvince { get; set; } // Autogenerate from Destination
        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string RecipientPostalCode { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Phone number must be between 8-14 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric.")]
        public virtual string RecipientPhoneNumber { get; set; }
        [StringLength(30)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email not valid")]
        public virtual string RecipientEmail { get; set; }



        public virtual int NumberOfPackages { get; set; }
        public virtual IList<ArrangeShipmentPackageViewModel> Packages { get; set; }

        public virtual string ShipmentPayer { get; set; } // Sender or Recipient
        public virtual string DTPayer { get; set; } // Duties & Taxes Payer

        public bool NotifySender { get; set; }
        public bool NotifyRecipient { get; set; }

        public virtual string CurrencyCode { get; set; }
        public virtual List<SelectListItem> CurrencyCodes { get; set; }
        public virtual decimal ShipmentCost { get; set; }  // Sum of PackageCosts
        
    }
}