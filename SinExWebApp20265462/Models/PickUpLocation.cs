using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("PickUpLocation")]
    public class PickUpLocation
    {
        public virtual int PickUpLocationID { get; set; }
        [Required]
        public virtual string PickUpNickName { get; set; }
        [StringLength(50)]
        public virtual string PickUpBuilding { get; set; }
        [StringLength(35)]
        public virtual string PickUpStreet { get; set; }
        [StringLength(25)]
        public virtual string PickUpCity { get; set; }
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Only 2-character province code is accepted.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are accepted.")]
        public virtual string PickUpProvinceCode { get; set; }
        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PickUpPostalCode { get; set; }
    }
}