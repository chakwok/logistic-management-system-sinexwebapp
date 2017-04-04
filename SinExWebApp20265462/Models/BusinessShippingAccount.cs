using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    public class BusinessShippingAccount : ShippingAccount
    {
        [Required]
        [StringLength(70)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public string ContactPersonName { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string DepartmentName { get; set; }
    }
}