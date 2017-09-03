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
        // Handles the event of the 'Create Character' button being clicked:
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string strNewURL = null;

            // Reset the customer validator state:
            cvLoginError.IsValid = true;
            loginError.Visible = false;

            try
            {
                // Get the login details from the user's input:
                string userName = inputUserName.Value.ToString();
                string password = inputPassword.Value.ToString();

                // Initalise appUser values:
                char appUserType = '?';
                int appUserID = -1;

                // Attempt to retrieve an appUser:
                AppUser appUser = BLLogin.GetAppUser(userName, password);
                
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
                            Session["AppUser"] = appUser;
                            Session["Manager"] = manager;
                            strNewURL = "~/UL/ULManagerDashboard.aspx";
                        }
                        else
                        {
                            // GENERATE VALIDATION ERROR HERE ABOUT REQUEST APPROVAL.
                        }
                        break;
                    case 'I':
                        Inspector inspector = BLLogin.GetInspector(appUserID);
                        Session["AppUser"] = appUser;
                        Session["Inspector"] = inspector;
                        strNewURL = "~/UL/ULInspectorDashboard.aspx";
                        break;
                    case 'A':
                        strNewURL = "~/UL/ULAdminDashboard.aspx";
                        Administrator administrator = BLLogin.GetAdministrator(appUserID);
                        Session["AppUser"] = appUser;
                        Session["Administrator"] = administrator;
                        strNewURL = "~/UL/ULAdminDashboard.aspx";
                        break;
                    default:
                        // GENERATE VALIDATION ERROR HERE ABOUT ACCOUNT NON-EXISTENCE HERE.
                        cvLoginError.IsValid = false;
                        loginError.Visible = true;
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