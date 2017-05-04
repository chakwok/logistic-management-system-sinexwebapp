using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.Models
{
    [Table("RecipientAddress")]
    public class RecipientAddress
    {

        public RecipientAddress()
        {

        }

        public RecipientAddress(int recipientAddressID, string nickName, string recipientName, string recipientShippingAccountId, string companyName, string departmentName, string building, string street, string city, string provinceCode, string postalCode, string recipientPhoneNumber, string recipientEmail)
        {
            RecipientAddressID = recipientAddressID;
            NickName = nickName;
            RecipientName = recipientName;
            RecipientShippingAccountId = recipientShippingAccountId;
            CompanyName = companyName;
            DepartmentName = departmentName;
            Building = building;
            Street = street;
            City = city;
            ProvinceCode = provinceCode;
            PostalCode = postalCode;
            RecipientPhoneNumber = recipientPhoneNumber;
            RecipientEmail = recipientEmail;
        }

        [Key]
        public virtual int RecipientAddressID { get; set; }
        public virtual string NickName { get; set; }
        public virtual string RecipientName { get; set; }
        public virtual string RecipientShippingAccountId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual string Building { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string ProvinceCode { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string RecipientPhoneNumber { get; set; }
        public virtual string RecipientEmail { get; set; }

    }
}