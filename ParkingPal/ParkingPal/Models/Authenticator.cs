using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public static class Authenticator
    {
        public static string AuthenticateUser(AppUser appUser, string pageURL)
        {
            // Set the lists of URLs each role can visit:
            List<string> managerURLs = new List<string>
            {
                "/UL/ULManagerDashboard.aspx",
                "/UL/ULLogout.aspx"
            };
            List<string> inspectorURLs = new List<string>
            {
                "/UL/ULInspectorDashboard.aspx",
                "/UL/ULLogout.aspx"
            };
            List<string> administratorURLs = new List<string>
            {
                "/UL/ULAdminDashboard.aspx",
                "/UL/ULLogout.aspx"
            };
            List<string> unauthenticatedURLs = new List<string>
            {
                "/UL/ULLogin.aspx",
                "/UL/ULPurchaseTicketDashboard.aspx",
                "/UL/ULPurchaseTicketPayment.aspx",
                "/UL/ULProcessTicketAndPayment.aspx",
                "/UL/ULHome.aspx"
            };

            // Determine the appUserType:
            char appUserType = '?';
            if(appUser != null)
            {
                appUserType = appUser.AppUserType;
            }

            // Determine if the user is allowed to view the current page:
            string redirect = null;
            switch (appUserType)
            {
                case 'M':
                    if(!managerURLs.Contains(pageURL))
                    {
                        redirect = "/UL/ULManagerDashboard.aspx";
                    }
                    break;
                case 'I':
                    if (!inspectorURLs.Contains(pageURL))
                    {
                        redirect = "/UL/ULInspectorDashboard.aspx";
                    }
                    break;
                case 'A':
                    if (!administratorURLs.Contains(pageURL))
                    {
                        redirect = "/UL/ULAdminDashboard.aspx";
                    }
                    break;
                case '?':
                    if (!unauthenticatedURLs.Contains(pageURL))
                    {
                        redirect = "/UL/ULHome.aspx";
                    }
                    break;
            }
            return redirect;
        }
    }
}