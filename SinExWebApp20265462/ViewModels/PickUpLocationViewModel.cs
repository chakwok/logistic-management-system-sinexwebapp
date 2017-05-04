using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class PickUpLocationViewModel
    {
        public PickUpLocationViewModel()
        {
        }

        public PickUpLocationViewModel(int pickUpLocationID, string pickUpNickName, string pickUpBuilding, string pickUpStreet, string pickUpCity, string pickUpProvinceCode, string pickUpPostalCode)
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
        [Required]
        public virtual string PickUpNickName { get; set; }
        [StringLength(50)]
        public virtual string PickUpBuilding { get; set; }
        [Required]
        [StringLength(35)]
        public virtual string PickUpStreet { get; set; }

        public virtual string PickUpCity { get; set; }
        public virtual List<SelectListItem> PickUpCities { get; set; }

        public virtual string PickUpProvinceCode { get; set; }

        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PickUpPostalCode { get; set; }
    }
}