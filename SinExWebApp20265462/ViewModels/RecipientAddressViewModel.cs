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

        public RecipientAddressViewModel(int recipientAddressID, string nickName, string recipientName, string recipientShippingAccountId, string companyName, string departmentName, string building, string street, string city, string provinceCode, string postalCode, string recipientPhoneNumber, string recipientEmail)
        {
            RecipientAddressID = recipientAddressID;
            NickName = nickName;
            RecipientName = recipientName;
            RecipientShippingAccountId = recipientShippingAccountId;
            CompanyName = companyName;
            DepartmentName = departmentName;
            Building = building;
            Street = street;
            City = city;
            ProvinceCode = provinceCode;
            PostalCode = postalCode;
            RecipientPhoneNumber = recipientPhoneNumber;
            RecipientEmail = recipientEmail;
        }

        public virtual int RecipientAddressID { get; set; }
        [Required]
        public virtual string NickName { get; set; }
        [Required]
        [StringLength(70)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public virtual string RecipientName { get; set; }
        [RegularExpression("[0-9]{12,12}", ErrorMessage = "Please enter a 12-digit number.")]
        public virtual string RecipientShippingAccountId { get; set; }
        [StringLength(40)]
        public virtual string CompanyName { get; set; }
        [StringLength(30)]
        public virtual string DepartmentName { get; set; }
        [StringLength(50)]
        public virtual string Building { get; set; }
        [Required]
        [StringLength(35)]
        public virtual string Street { get; set; }

        public virtual string City { get; set; }
        public virtual List<SelectListItem> Cities { get; set; }

        public virtual string ProvinceCode { get; set; }

        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PostalCode { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Phone number must be between 8-14 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric.")]
        public virtual string RecipientPhoneNumber { get; set; }
        [StringLength(30)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email not valid")]
        public virtual string RecipientEmail { get; set; }

    }
}