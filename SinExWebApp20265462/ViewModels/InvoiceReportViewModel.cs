using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.ViewModels
{
    public class InvoiceReportViewModel
    {
        public virtual InvoiceSearchViewModel Invoice { get; set; }
        public virtual IEnumerable<InvoiceListViewModel> Invoices { get; set; }
    }
}