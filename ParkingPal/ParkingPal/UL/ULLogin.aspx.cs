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
                strNewURL = "~/UL/ULHome.aspx?" + appUserType;
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