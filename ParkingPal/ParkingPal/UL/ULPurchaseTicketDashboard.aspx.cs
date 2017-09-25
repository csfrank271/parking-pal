using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using ParkingPal.BL;
using System.Web.Services;

namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketDashboard : System.Web.UI.Page
    {
        int loading = 0;
        string strNewURL = null;

        protected void NavigateToPayment (object sender, EventArgs e) {
            if (String.IsNullOrWhiteSpace(this.inputUserEmailAddress.Value) 
                || String.IsNullOrWhiteSpace(this.inputUserRego.Value) 
                || String.IsNullOrWhiteSpace(this.inputTicketStartTime.Value)
                || String.IsNullOrWhiteSpace(this.labelTicketEndTime.InnerText)
                || String.IsNullOrWhiteSpace(this.selectCarparkType.Value)
                || String.IsNullOrWhiteSpace(this.selectCarparkOptions.Value) )
            {
                throw new Exception();
            } else
            {
                // make call to dl to get ticket price and rate
                var rate = BLPurchaseTicket.GetRate(Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), 6, this.selectCarparkType.Value);
              //  var rate = BLPurchaseTicket.GetRate(Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), this.selectCarparkOptions.Value, this.selectCarparkType.Value);
                var carpark = this.selectCarparkOptions.Value;
                Ticket ticket = new Ticket(-1, this.inputUserRego.Value, Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), 6.00, "UON", "General");
                strNewURL = "~/UL/ULPurchaseTicketPayment.aspx";
                Session["Ticket"] = ticket;
                Response.Redirect(strNewURL);
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
                } else
                {
                    if (!this.inputTicketStartTime.Value.Any())
                    {
                        this.inputTicketStartTime.Value = DateTime.Now.ToShortTimeString();
                    }
                    if (!this.labelTicketEndTime.InnerText.Any())
                    {
                        this.labelTicketEndTime.InnerText = DateTime.Now.AddMinutes(30).ToShortTimeString();
                    }
                }
            }             
        }

      
    }
}