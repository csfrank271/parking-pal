using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using ParkingPal.BL;
using System.Web.Services;

namespace ParkingPal.UL
{
    public partial class ULManagerDashboard : System.Web.UI.Page
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
                    Manager manager = (Manager)Session["Manager"];
                    List<InspectorUser> inspectorUsers = BLManagerDashboard.
                        GetManagerInspectors(manager.ManagerID);
                    if(inspectorUsers != null)
                    {
                        LVInspectorUsers.DataSource = inspectorUsers;
                        LVInspectorUsers.DataBind();
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

        [WebMethod]
        protected void UpdateInspectorsList (ListViewDataItem e)
        {
            LVInspectorUsers.SelectedIndex = e.DataItemIndex;
            LVInspectorUsers.DataBind();

            // Update selected inspector panel:
            //InspectorsName.InnerText =
        }
    }
}