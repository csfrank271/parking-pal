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
                // Get the login details from the user's input:
                string userName = inputUserName.Value.ToString();
                string password = inputPassword.Value.ToString();

                // Initalise appUser values:
                char appUserType = '?';
                int appUserID = -1;

                // Attempt to retrieve an appUser:
                AppUser appUser = BLLogin.GetAppUser(
                    userName, password);
                
                // If the appUser was retrieved:
                if (appUser != null)
                {
                    appUserType = appUser.AppUserType;
                    appUserID = appUser.AppUserID;
                }

                // Make a decision based on appUser's role:
                switch(appUserType)
                {
                    case 'M':
                        Manager manager = BLLogin.GetManager(appUserID);
                        if(manager.ApprovalStatus == 'C')
                        {
                            strNewURL = "~/UL/ULManagerDashboard.aspx";
                            Session["AppUser"] = appUser;
                            Session["Manager"] = manager;
                        }
                        else
                        {
                            // GENERATE VALIDATION ERROR HERE ABOUT REQUEST APPROVAL.
                        }
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