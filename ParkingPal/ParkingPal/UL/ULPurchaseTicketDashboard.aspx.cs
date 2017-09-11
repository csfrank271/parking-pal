using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketDashboard : System.Web.UI.Page
    {
        int loading = 0;
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
            }             
        }
    }
}