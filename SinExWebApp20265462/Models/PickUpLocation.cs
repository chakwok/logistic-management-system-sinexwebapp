using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.Models
{
    [Table("PickUpLocation")]
    public class PickUpLocation
    {
        public PickUpLocation()
        {

        }
        public PickUpLocation(int pickUpLocationID, string pickUpNickName, string pickUpBuilding, string pickUpStreet, string pickUpCity, string pickUpProvinceCode, string pickUpPostalCode)
        {
            PickUpLocationID = pickUpLocationID;
            PickUpNickName = pickUpNickName;
            PickUpBuilding = pickUpBuilding;
            PickUpStreet = pickUpStreet;
            PickUpCity = pickUpCity;
            PickUpProvinceCode = pickUpProvinceCode;
            PickUpPostalCode = pickUpPostalCode;
        }

        public virtual int PickUpLocationID { get; set; }
        public virtual string PickUpNickName { get; set; }
        public virtual string PickUpBuilding { get; set; }
        public virtual string PickUpStreet { get; set; }
        public virtual string PickUpCity { get; set; }
        public virtual string PickUpProvinceCode { get; set; }
        public virtual string PickUpPostalCode { get; set; }
    }
}