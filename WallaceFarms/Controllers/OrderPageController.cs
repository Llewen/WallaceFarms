using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WallaceFarms;
using WallaceFarms.Models;

namespace WallaceFarms.Controllers
{
    public class OrderPageController : OrderController
    {

        [HttpGet]
        public ActionResult Index()
        {
            int ID;
            int BeefID;

            try
            {
                ID = (from o in entities.Orders select o.OrderID).Max();
            }
            catch
            {
                ID = 0;
            }

            try
            {
                BeefID = (from o in entities.BeefOrders select o.BeefOrderID).Max();
            }
            catch
            {
                BeefID = 0;
            }
            OrderModel order = new OrderModel();
            order.theOrder = new Order();
            order.theBeefOrder = new BeefOrder();

            order.theOrder.OrderID = ID + 1;
            order.theBeefOrder.BeefOrderID = BeefID + 1;
            order.theBeefOrder.OrderID = order.theOrder.OrderID;
            return View("OrderPage", order);
        }


        [HttpPost]
        public ActionResult Index(OrderModel Order)
        {
            bool inDatabase;

            entities.Orders.Add(Order.theOrder);
            entities.BeefOrders.Add(Order.theBeefOrder);

            try
            {
                entities.SaveChanges();
                inDatabase = true;
            }
            // Handy code by Troy Alford and Richard of Stackoverflow.com
            // Detects the specific database error and provides a brief description
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                string errorMessage = "Entity Validation Failed - errors follow:\n" + sb.ToString();

                inDatabase = false;
            }

            if (inDatabase)
            {
                // Send a notification email to Professor Wallace and redirect to thank you page.
                MailServer mailserver = new MailServer();
                mailserver.SendOrderMail(Order);
                return Redirect("/Thanks");
            }
            else
            {
                return Redirect("/Error");
            }
        }

    }

    public class MailServer
    {
        /// <summary>
        /// Recipient of order notifications. (Professor Wallace)
        /// </summary>
        // If you wish to add more recipients, just create more string variables like To
        // and put another line like Message.To.Add(RECIPIENT_STRING_VARIABLE); in the constructor.
        private const string OrderNotificationRecipient = "smcaldwe@geneva.edu";

        /// <summary>
        /// Gmail username for the email sender. After all, these emails need to come from somewhere.
        /// This can be changed to any Gmail username.
        /// </summary>
        private const string MailServerUsername = "wallace.farm.mailserver";

        /// <summary>
        /// Gmail password for the email sender. After all, these emails need to come from somewhere.
        /// This can be changed to match any Gmail account.
        /// </summary>
        private const string MailServerPassword = "!WallaceCSC309";

        /// <summary>
        /// SMTP (Simple Mail Transfer Protocol) client.
        /// This is Gmail by default, but it can be changed to any SMTP service.
        /// </summary>
        private SmtpClient Smtp { get; set; }

        /// <summary>
        /// Contents of the email.
        /// </summary>
        private MailMessage Message { get; set; }

        public MailServer()
        {
            Smtp = new SmtpClient("smtp.gmail.com", 587);
            Smtp.EnableSsl = true;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = new System.Net.NetworkCredential(MailServerUsername, MailServerPassword);

            Message = new MailMessage();
            Message.From = new MailAddress(MailServerUsername + "@gmail.com");
        }

        /// <summary>
        /// General method to send an email with a specified subject and body.
        /// </summary>
        /// <param name="to">Recipient of the email.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email.</param>
        /// <returns>Returns whether the email was successfully sent.</returns>
        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                Message.To.Add(to);
                Message.Subject = subject;
                Message.Body = body;

                Smtp.Send(Message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to send a notification email based on a particular order.
        /// </summary>
        /// <param name="order">The order that the notification is about.</param>
        /// <returns>Returns (from the SendMail method) whether the email was successfully sent.</returns>
        public bool SendOrderMail(OrderModel order)
        {
            string name = order.theOrder.Name;
            int? amount = order.theBeefOrder.NumQuarters;

            string body = name + " just ordered " + amount + (amount == 1 ? " quarter" : " quarters") + " of beef!";

            return SendMail(OrderNotificationRecipient, "New Beef Order!", body);
        }
    }
}
