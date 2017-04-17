using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("Address")]
    public class Address
    {
        public virtual int AddressID { get; set; }
        public virtual string address { get; set; }
        [Required]
        public virtual string nickname { get; set; }
    }
}