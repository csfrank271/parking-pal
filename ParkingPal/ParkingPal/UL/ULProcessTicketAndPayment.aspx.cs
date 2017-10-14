using ParkingPal.BL;
using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.UL
{
    public partial class ULProcessTicketAndPayment : System.Web.UI.Page
    {
        Ticket ticket;
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
                strNewUrl = "~/UL/ULPurchaseTicketDashboard.aspx";
            }
            finally
            {
                if (strNewUrl != null)
                {
                    Response.Redirect(strNewUrl);
                } else
                {
                   ticket = (Ticket)Session["Ticket"]; 
                    if (ticket == null)
                    {
                        strNewUrl = "~/UL/ULPurchaseTicketDashboard.aspx";
                        Response.Redirect(strNewUrl);
                    } else
                    {
                        ticket.Rate = ticket.RateObject.HalfHourlyRate;
                        int newTicketID = BLPurchaseTicket.AddTicket(ticket);
                        //insert ticket into db and return ticket id
                        foreach (var payment in ticket.Payments)
                        {
                            payment.ticketID = newTicketID;
                            payment.status = "success";
                            int newPaymentID = BLPurchaseTicket.AddPayment(payment, ticket);
                        }
                        this.ticketid.InnerText = newTicketID.ToString();
                        Session.Clear();
                    }
                }
            }
        } 
    }
}