using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.UL
{
    public partial class ULError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["exception"] != null)
            {
                Exception exception = (Exception)Session["exception"];
                exceptionMsg.InnerText = exception.TargetSite + " " + exception.StackTrace + " " + exception.Message;
                Session.Remove("exception");
            }
            else
            {
                exceptionMsg.InnerText = "No errors.";
            }
        }
    }
}