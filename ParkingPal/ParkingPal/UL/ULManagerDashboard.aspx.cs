using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.UL
{
    public partial class ULManagerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewURL = null;

            try
            {
                if (Session["User"] == null)
                {
                    throw new Exception("You are not authenticated to access this page.");
                }
                else
                {
                   
                }
            }
            catch (Exception exception)
            {
                strNewURL = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }

            // Redirect to the next page:
            if (strNewURL != null)
            {
                Response.Redirect(strNewURL);
            }
        }
    }
}