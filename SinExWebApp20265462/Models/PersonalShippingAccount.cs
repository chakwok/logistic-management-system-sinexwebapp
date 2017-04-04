using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    public class PersonalShippingAccount : ShippingAccount
    {
        [Required]
        [StringLength(35)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(35)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public string LastName { get; set; }
    }
}