using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("RecipientAddress")]
    public class RecipientAddress
    {
        public virtual int RecipientAddressID { get; set; }
        [Required]
        public virtual string NickName { get; set; }
        [StringLength(50)]
        public virtual string Building { get; set; }
        [StringLength(35)]
        public virtual string Street { get; set; }
        [StringLength(25)]
        public virtual string City { get; set; }
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Only 2-character province code is accepted.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are accepted.")]
        public virtual string ProvinceCode { get; set; }
        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PostalCode { get; set; }

    }
}