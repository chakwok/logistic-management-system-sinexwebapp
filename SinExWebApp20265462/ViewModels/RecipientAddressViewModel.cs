using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20265462.Models;

namespace SinExWebApp20265462.ViewModels
{
    public class RecipientAddressViewModel
    {
        public RecipientAddressViewModel()
        {
        }

        public RecipientAddressViewModel(int recipientAddressID, string nickName, string building, string street, string city, string provinceCode, string postalCode)
        {
            RecipientAddressID = recipientAddressID;
            NickName = nickName;
            Building = building;
            Street = street;
            City = city;
            ProvinceCode = provinceCode;
            PostalCode = postalCode;
        }
        public virtual int RecipientAddressID { get; set; }
        [Required]
        public virtual string NickName { get; set; }
        [StringLength(50)]
        public virtual string Building { get; set; }
        [StringLength(35)]
        public virtual string Street { get; set; }

        public virtual string City { get; set; }
        public virtual List<SelectListItem> Cities { get; set; }

        public virtual string ProvinceCode { get; set; }

        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PostalCode { get; set; }

    }
}