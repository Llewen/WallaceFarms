﻿using System;
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
            entities.Orders.Add(Order.theOrder);
            entities.BeefOrders.Add(Order.theBeefOrder);
            try
            {
                entities.SaveChanges();
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

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            // Send a notification email to Professor Wallace.
            MailServer mailserver = new MailServer();
            mailserver.SendOrderMail(Order);

            return Redirect("/Index");
        }

    }

    public class MailServer
    {
        /// <summary>
        /// Recipient of order notifications. (Professor Wallace)
        /// </summary>
        // If you wish to add more recipients, just create more string variables like To
        // and put another line like Message.To.Add(RECIPIENT_STRING_VARIABLE); in the constructor.
        private const string To = "smcaldwe@geneva.edu";

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
            Message.To.Add(To);
            Message.From = new MailAddress(MailServerUsername + "@gmail.com");
        }

        public bool SendMail(string subject, string body)
        {
            try
            {
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

        public bool SendOrderMail(OrderModel order)
        {
            string name = order.theOrder.Name;
            int? amount = order.theBeefOrder.NumQuarters;

            string body = name + " just ordered " + amount + (amount == 1 ? " quarter" : " quarters") + " of beef!";

            return SendMail("New Beef Order!", body);
        }
    }
}
