using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.BL;

namespace ParkingPal.UL
{
    public partial class ULLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Handles the event of the 'Create Character' button being clicked:
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string strNewURL = null;

            try
            {
                string userName = inputUserName.Value.ToString();
                string password = inputPassword.Value.ToString();
                char appUserType = BL.BLLogin.GetAppUserType(
                    userName, password);
                switch(appUserType)
                {
                    case 'M':
                        strNewURL = "~/UL/ULManagerDashboard.aspx";
                        // Get Manager object.
                        break;
                    case 'I':
                        strNewURL = "~/UL/ULInspectorDashboard.aspx";
                        // Get Manager object.
                        break;
                    case 'A':
                        strNewURL = "~/UL/ULAdminDashboard.aspx";
                        // Get Manager object.
                        break;
                    default:
                        // Inform user of error 
                        break;
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