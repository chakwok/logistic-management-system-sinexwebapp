using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20265462.Models
{
    [Table("Shipment")]
    public class Shipment
    {
        // Reminder: Store in Db Money => RMB
        [Key]
        public virtual int WaybillId { get; set; }

        public virtual string ReferenceNumber { get; set; }

        public virtual string ServiceType { get; set; }
        public virtual DateTime ShippedDate { get; set; }
        public virtual DateTime DeliveredDate { get; set; }

        public virtual int ShippingAccountId { get; set; } // Sender
        public virtual ShippingAccount ShippingAccount { get; set; } // Sender
        public virtual string Origin { get; set; } // Sender's City

        public virtual string RecipientName { get; set; }
        public virtual int RecipientShippingAccountId { get; set; } // If Payer is Recipient
        public virtual string RecipientCompanyName { get; set; } // If any
        public virtual string RecipientDepartmentName { get; set; } // If any
        public virtual string RecipientBuilding { get; set; } // If any
        public virtual string RecipientStreet { get; set; }
        public virtual string Destination { get; set; } // Recipient's City
        public virtual string RecipientProvince { get; set; } // Autogenerate from Destination
        public virtual string RecipientPostalCode { get; set; }
        public virtual string RecipientPhoneNumber { get; set; }
        public virtual string RecipientEmail { get; set; }

        public virtual int NumberOfPackages { get; set; }
        public virtual IList<Package> Packages { get; set; }

        public virtual string ShipmentPayer { get; set; }  // Sender or Recipient
        public virtual string DTPayer { get; set; } // Duties & Taxes Payer

        public bool NotifySender { get; set; }
        public bool NotifyRecipient { get; set; }

        public virtual string Status { get; set; } // Pending; Cancelled; Confirmed; Picked up; Shipped; Delivered

        public virtual int Pickup { get; set; }
        
        public virtual decimal ShipmentCost { get; set; }  // Sum of PackageCosts
        public virtual decimal DutiesCost { get; set; }
        public virtual decimal TaxesCost { get; set; }

        public virtual string AuthorizationCode { get; set; } // After payment, sent by Credit Card Authorization Authority.
    }
}
