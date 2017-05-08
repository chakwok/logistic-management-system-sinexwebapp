using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.ViewModels
{
    public class InvoiceListViewModel
    {
        public virtual int WaybillId { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual DateTime ShippedDate { get; set; }
        public virtual string RecipientName { get; set; }
        public virtual decimal NumberOfInvoice { get; set; }
        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        public virtual int ShippingAccountId { get; set; }
    }
}