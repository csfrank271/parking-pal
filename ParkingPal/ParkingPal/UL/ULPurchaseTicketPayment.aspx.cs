using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketPayment : System.Web.UI.Page
    {
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
                    Ticket ticket = (Ticket)Session["Ticket"];
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
                        this.divLocation.InnerText = ticket.ParkingLotLocation != null ? ticket.ParkingLotLocation.Address : "";
                        this.divType.InnerText = ticket.CarparkType.Any() ? ticket.CarparkType : "";
                        this.spanTotalPrice.InnerText = "52.00";
                    }
                }
            }
        }
    }
}