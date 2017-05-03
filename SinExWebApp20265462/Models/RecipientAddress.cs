﻿using System;
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

        public RecipientAddress(int recipientAddressID, string nickName, string building, string street, string city, string provinceCode, string postalCode)
        {
            RecipientAddressID = recipientAddressID;
            NickName = nickName;
            Building = building;
            Street = street;
            City = city;
            ProvinceCode = provinceCode;
            PostalCode = postalCode;
        }

        [Key]
        public virtual int RecipientAddressID { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Building { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string ProvinceCode { get; set; }
        public virtual string PostalCode { get; set; }

    }
}