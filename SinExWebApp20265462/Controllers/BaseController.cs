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

        public void SendEmail()
        {
            // Create an instance of MailMessage named mail.
            MailMessage mail = new MailMessage();

            // Create an instance of SmtpClient named emailServer.
            // Set the mail server to use as "smtp.ust.hk".
            SmtpClient emailServer = new SmtpClient("smtp.cse.ust.hk");

            // Set the sender (From), receiver (To), subject and 
            // message body fields of the mail message.
            mail.From = new MailAddress("comp3111_team119@cse.ust.hk", "comp3111_team119");
            mail.To.Add("comp3111_team119@cse.ust.hk");

            mail.Subject = "Testing Mail";
            mail.Body = "Hello??";
            // Send the message.

            emailServer.Send(mail);
        }
    }
}