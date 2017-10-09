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
        static ParkingLot parkingLot;
        static ParkingBay parkingBay;
        static Ticket ticket;
        static Payment payment;
         
        [ScriptMethod, WebMethod]
        public static void PaymentCompleted()
        { 
            var test = 2;
            ticket.Rate = ticket.RateObject.HalfHourlyRate;
            var newTicketID = 0;
            //insert ticket into db and return ticket id
             foreach (var payment in ticket.Payments)
            {
                payment.ticketID = newTicketID;
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
                        parkingLot = DALPurchaseTicket.GetParkingLot(Convert.ToInt16(ticket.ParkingLotLocation));
                        parkingBay = DALPurchaseTicket.GetCarparkType(Convert.ToInt16(ticket.CarparkType), Convert.ToInt16(ticket.ParkingLotLocation));
                        this.divLocation.InnerText = parkingLot.ShortName;
                        this.divType.InnerText = parkingBay.CarparkType;
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