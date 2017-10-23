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
    public partial class ULHome : System.Web.UI.Page
    {
        [WebMethod]
        protected void extendTicket (object sender, EventArgs e)
        {
            var something = "sdf";
            if (BL.BLTicket.GetTicketInfo(Convert.ToInt16(this.inputTicketId.Value)).TicketID > 0)
            {

            }
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewURL = null;

            // Authenticate the user:
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageURL = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageURL);
                if (redirect != null)
                {
                    strNewURL = "~" + redirect;
                }
                else
                {
                    // Code to run if user is authenticated.
                }
            }
            catch (Exception exception)
            {
                strNewURL = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }
            finally
            {
                // Redirect to the next page:
                if (strNewURL != null)
                {
                    Response.Redirect(strNewURL);
                }
            }
        }
    }
}