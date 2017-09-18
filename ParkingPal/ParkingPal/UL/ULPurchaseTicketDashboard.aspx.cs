using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using System.Web.Services;

namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketDashboard : System.Web.UI.Page
    {
        int loading = 0;
        string strNewURL = null;

        protected void NavigateToPayment (object sender, EventArgs e) {
            Ticket ticket = new Ticket(-1, this.inputUserRego.Value, Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), null, "Disable");
            strNewURL = "~/UL/ULPurchaseTicketPayment.aspx";
            Session["Ticket"] = ticket;
            Response.Redirect(strNewURL); 
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