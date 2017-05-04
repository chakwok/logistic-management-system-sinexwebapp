using Microsoft.AspNet.Identity;
using SinExWebApp20265462.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.Controllers
{
    public class BaseController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        /*
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
        */

        public decimal ConvertCurrency (string CurrencyCode, decimal Fee)
        {
            if (CurrencyCode == null) return Fee;

            decimal result = Fee;
            string key = CurrencyCode + "ExchangeRate";

            if (Session[key] == null)
            {
                Session.Add(key, db.Currencies.Find(CurrencyCode).ExchangeRate);
            }

            result = result * Decimal.Parse(Session[key].ToString()); ;

            return result;
        }

        public bool SaveToSessionState<T>(string key, T obj)
        {
            if (Session[key] == null)
            {
                Session.Add(key, obj);
                return true;
            }
            return false;
        }

        public string GetProvince(string City)
        {
            string province;

            province = db.Destinations.First(d => d.City == City).ProvinceCode;

            return province;
        }

        public int GetUserId()
        {
            string userName = User.Identity.GetUserName();
            string key = userName + "Id";

            if (Session[key] == null)
            {
                int? ShippingAccountId = db.ShippingAccounts.First(s => s.UserName == userName).ShippingAccountId;
                if (ShippingAccountId != null) Session.Add(key, ShippingAccountId);
                else return -1;
            }
            
            return Int32.Parse(Session[key].ToString());
        }

        public string GetUserCity(int userId)
        {
            string key = "user" + userId + "City";
            if (Session[key] == null)
            {
                string userCity = db.ShippingAccounts.Find(userId).City;
                if (userCity != null) Session.Add(key, userCity);
                else return "";
            }
            return Session[key].ToString();
        }

        public string ShowShippingAccountId (int ShippingAccountId)
        {
            var digit = "";
            int count = ShippingAccountId.ToString().Length;
            for ( int i =0; i <12 - count; i++)
            {
                digit += 0;
            }
            return (digit + ShippingAccountId.ToString());
        }

        public string ShowWaybillId(int WaybillId)
        {
            var digit = "";
            int count = WaybillId.ToString().Length;
            for (int i = 0; i < 16 - count; i++)
            {
                digit += 0;
            }
            return (digit + WaybillId.ToString());
        }

        public SelectList PopulateCurrenciesDropDownList()
        {
            var currencyQuery = db.Currencies.Select(s => s.CurrencyCode).Distinct().OrderBy(c => c);
            return new SelectList(currencyQuery);
        }

        public SelectList PopulateDestinationsDropDownList()
        {
            var destinationQuery = db.Destinations.Select(s => s.City).Distinct().OrderBy(c => c);
            return new SelectList(destinationQuery);
        }

        public SelectList PopulateServiceTypesDropDownList()
        {
            var serviceTypeQuery = db.ServiceTypes;
            return new SelectList(serviceTypeQuery, "ServiceTypeID", "Type", new { selectedValue = "ServiceTypeID" });
        }

        public SelectList PopulatePackageTypeSizesDropDownList()
        {
            var packageTypeSizeQuery = db.PackageTypeSizes;
            return new SelectList(packageTypeSizeQuery, "PackageTypeSizeID", "Size", "PackageType.Type", new { selectedValue = "PackageTypeSizeID" });
        }

        public SelectList PopulateRecipientAddressesDropDownList()
        {
            var recipientAddressQuery = db.RecipientAddresses;
            return new SelectList(recipientAddressQuery, "RecipientAddressID", "NickName", new { selectedValue = "RecipientAddressID" });
        }

        public void SendEmail(string recipient, string username, string confrimURL)
        {
            //CURRENTLY RECIPIENT MUST BE COMP3111....
            //to prevent non exisiting email

            // Create an instance of MailMessage named mail.
            MailMessage mail = new MailMessage();

            // Create an instance of SmtpClient named emailServer.
            // Set the mail server to use as "smtp.ust.hk".
            SmtpClient emailServer = new SmtpClient("smtp.cse.ust.hk");

            // Set the sender (From), receiver (To), subject and 
            // message body fields of the mail message.
            mail.From = new MailAddress("comp3111_team119@cse.ust.hk", "comp3111_team119");
            mail.To.Add(recipient);

            mail.Subject = "SinEx Account Confirmation";
            mail.Body = "Dear "+ username + ",\nThank you for signing up for our SinEx service \nPress the link below to verify your registration\n" + confrimURL; 
            //mail.IsBodyHtml = true;
            // Send the message.
            emailServer.Send(mail);
        }
    }
}