using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("Destination")]
    public class Destination
    {
        public virtual int DestinationID { get; set; }
        [Required]
        [StringLength(25)]
        public string City { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Only 2-character province code is accepted.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are accepted.")]
        public string ProvinceCode { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual Currency Currency { get; set; }
    }
}