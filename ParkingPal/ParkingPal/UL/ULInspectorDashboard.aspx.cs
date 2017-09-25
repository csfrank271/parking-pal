using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.BL;
using ParkingPal.Models;

namespace ParkingPal.UL
{
    public partial class ULInspectorDashboard : System.Web.UI.Page
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
                    Inspector inspector = (Inspector)Session["Inspector"];
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

        // Searches for Tickets associated with a registration number.
        protected void SearchForTickets(object sender, EventArgs e)
        {
            string rego = Tbx_RegistrationNum.Text;
            List<Ticket> tickets = BLInspectorDashboard.GetCurrentTicketsForRego(rego);
            LVTickets.DataSource = tickets;
            LVTickets.DataBind();
        }
    }
}