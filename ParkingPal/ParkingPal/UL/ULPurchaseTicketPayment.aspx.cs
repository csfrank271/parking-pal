using ParkingPal.BL;
using ParkingPal.DAL;
using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketPayment : System.Web.UI.Page
    { 
        static Ticket ticket;
        static Payment payment;
         
        [ScriptMethod, WebMethod]
        public static void PaymentCompleted()
        { 
            var test = 2;
            ticket.Rate = ticket.RateObject.HalfHourlyRate; 
            int newTicketID = BLPurchaseTicket.AddTicket(ticket); 
            //insert ticket into db and return ticket id
            foreach (var payment in ticket.Payments)
            {
                payment.ticketID = newTicketID;
                int newPaymentID = BLPurchaseTicket.AddPayment(payment);
                // insert into db
            } 
 
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewUrl = null;
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageURL = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageURL);
                if (redirect != null)
                {
                    strNewUrl = "~" + redirect;
                }
            }
            catch (Exception exception)
            {
                strNewUrl = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }
            finally
            {
                if (strNewUrl != null)
                {
                    Response.Redirect(strNewUrl);
                }
                else
                {
                    ticket = (Ticket)Session["Ticket"];
                    if (ticket == null)
                    {
                        var strUrl = "~/UL/ULPurchaseTicketDashboard.aspx";
                        Response.Redirect(strUrl);
                    }
                    else
                    {
                        this.divTicketDate.InnerText = ticket.StartDateTime != null ? ticket.StartDateTime.ToShortDateString() : "";
                        this.divStartTime.InnerText = ticket.StartDateTime != null ? ticket.StartDateTime.ToShortTimeString() : "";
                        this.divEndTime.InnerText = ticket.EndDateTime != null ? ticket.EndDateTime.ToShortTimeString() : "";
                        this.divRego.InnerText = ticket.Rego.Any() ? ticket.Rego : "";
                        ticket.ParkingLotObject = DALPurchaseTicket.GetParkingLot(Convert.ToInt16(ticket.ParkingLotLocation));
                        ticket.ParkingBayObject = DALPurchaseTicket.GetCarparkType(Convert.ToInt16(ticket.CarparkType), Convert.ToInt16(ticket.ParkingLotLocation));
                        this.divLocation.InnerText = ticket.ParkingLotObject.ShortName;
                        this.divType.InnerText = ticket.ParkingBayObject.CarparkType;
                        decimal total = 0;
                        foreach (var payment in ticket.Payments)
                        {
                            total += payment.total;
                        }
                        this.spanTotalPrice.InnerText = total.ToString(); 
                    }
                }
            }
        }
    }
}