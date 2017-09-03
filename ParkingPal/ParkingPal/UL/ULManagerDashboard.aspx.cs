using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;

namespace ParkingPal.UL
{
    public partial class ULManagerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewURL = null;

            try
            {
                if (Session["AppUser"] == null ||
                    Session["Manager"] == null)
                {
                    throw new Exception("You are not authenticated to access this page.");
                }
                else
                {
                    // Retrieve appUser and manager from the session:
                    AppUser appUser = (AppUser)Session["AppUser"];
                    Manager manager = (Manager)Session["Manager"];
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