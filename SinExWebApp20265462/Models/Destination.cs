using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("Destination")]
    public class Destination
    {
        public virtual int DestinationID { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual Currency Currency { get; set; }
    }
}