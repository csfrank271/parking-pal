using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.BL;
using ParkingPal.Models;

namespace ParkingPal.UL
{
    public partial class ULRegister : System.Web.UI.Page
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


        public void RegisterManager(object sender, EventArgs e)
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
                    // Reset all validators:
                    CV_InputConfirmPassword.IsValid = true;
                    CV_InputEmailAddress.IsValid = true;
                    CV_InputPassword.IsValid = true;

                    // Get the inputs:
                    string emailAddress = InputEmailAddress.Value,
                        firstName = InputFirstName.Value,
                        lastName = InputLastName.Value,
                        password = InputPassword.Value,
                        confirmPassword = InputConfirmPassword.Value;

                    Regex rgxUserNamePwd = new Regex(@"^[^ ]{1,50}$");
                    Regex rgxHumanName = new Regex(@"^[A-z]{1,50}$");
                    bool verificationPassed = true;

                    // Verify email address:
                    if (!BLCommon.IsValidEmail(emailAddress))
                    {
                        verificationPassed = false;
                        CV_InputEmailAddress.IsValid = false;
                    }

                    // Verify first name:
                    if (!rgxHumanName.IsMatch(firstName))
                    {
                        verificationPassed = false;
                        CV_InputFirstName.IsValid = false;
                    }

                    // Verify last name:
                    if (!rgxHumanName.IsMatch(lastName))
                    {
                        verificationPassed = false;
                        CV_InputLastName.IsValid = false;
                    }

                    // Verify password:
                    if (!rgxUserNamePwd.IsMatch(password))
                    {
                        verificationPassed = false;
                        CV_InputPassword.IsValid = false;
                    }
                    // Verify confirm password:
                    if (!rgxUserNamePwd.IsMatch(confirmPassword))
                    {
                        verificationPassed = false;
                        CV_InputConfirmPassword.IsValid = false;
                    }

                    // User can be registered:
                    if (verificationPassed)
                    {
                        ManagerUser managerUser = BLRegister.RegisterManager(emailAddress, password, firstName, lastName, 'C'); // Set to complete for demo. If continuing project, make this 'U'.
                        if (managerUser == null)
                        {
                            CV_RegManager.IsValid = false;
                        }
                        else
                        {
                            Session["AppUser"] = managerUser.AppUser;
                            Session["Manager"] = managerUser.Manager;
                            strNewURL = "~/UL/ULManagerDashboard.aspx";
                        }
                    }
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
