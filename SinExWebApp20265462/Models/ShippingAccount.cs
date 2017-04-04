using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("ShippingAccount")]
    public abstract class ShippingAccount
    {
        [Key]
        public virtual long ShippingAccountId { get; set; }
        [StringLength(10)]
        public virtual string UserName { get; set; }
        public virtual string AccountType { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Phone number must be between 8-14 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric.")]
        public virtual string PhoneNumber { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email not valid")]
        public virtual string Email { get; set; }
        [StringLength(50)]
        public virtual string Building { get; set; }
        [Required]
        [StringLength(35)]
        public virtual string Street { get; set; }
        [Required]
        [StringLength(25)]
        public virtual string City { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Only 2-character province code is accepted.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters are accepted.")]
        public virtual string Province { get; set; }
        [StringLength(6, MinimumLength = 5, ErrorMessage = "Postal code must be between 5-6 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
        public virtual string PostalCode { get; set; }
        [Required]
        public virtual string CreditCardType { get; set; }
        [Required]
        [StringLength(19, MinimumLength = 14, ErrorMessage = "Credit card number must be between 14-19 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Credit card number must be numeric.")]
        public virtual string CreditCardNumber { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Security number must be between 3-4 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Security Number must be numeric.")]
        public virtual string CreditCardSecurityNumber { get; set; }
        [Required]
        [StringLength(70)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public virtual string CardholderName { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Please enter a valid month.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Expiry month must be numeric.")]
        public virtual int CreditCardExpiryMonth { get; set; }
        [Required]
        [Range(17, 99, ErrorMessage = "Your credit card is expired.")]
        //[MaxLength(2, ErrorMessage = "Please enter a 2-digit number.")]
        [RegularExpression("[0-9]{2,2}", ErrorMessage = "Please enter a 2-digit number.")]
        public virtual int CreditCardExpiryYear { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}