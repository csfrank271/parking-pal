using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;

namespace ParkingPal.UL
{
    public partial class ULLogout : System.Web.UI.Page
    {
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
                    // Clear all session data to reset application to user:
                    Session.Abandon();
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