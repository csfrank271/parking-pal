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
                    if (inspectorUsers != null)
                    {
                        LVInspectorUsers.DataSource = inspectorUsers;
                        LVInspectorUsers.DataBind();
                    }
                    else
                    {
                        LVInspectorUsers.DataSource = null;
                        LVInspectorUsers.DataBind();
                    }
                    if(!IsPostBack)
                    {
                        LVSelectedInspector.DataSource = null;
                        LVSelectedInspector.DataBind();
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

        // Selects an Inspector from the Inpsector list and updates the selected Inspector panel.
        [WebMethod]
        public void SelectInspector(int selectedItemIndex)
        {
            // Update the selected item in the InspectorUsers list:
            LVInspectorUsers.SelectedIndex = selectedItemIndex;
            LVInspectorUsers.DataBind();

            // Retrieve the selected InspectorUser from the list's datasource:
            List<InspectorUser> inspectorUsers =
                (List<InspectorUser>)LVInspectorUsers.DataSource;
            List<InspectorUser> selectedInspectorUser = new List<InspectorUser>
            {
                inspectorUsers.ElementAt(selectedItemIndex)
            };

            // Update the selected Inspector panel;
            LVSelectedInspector.DataSource = selectedInspectorUser;
            LVSelectedInspector.DataBind();
        }

        // Deletes an Inspector from the Inpsector list and updates the selected Inspector panel.
        [WebMethod]
        public void DeleteInspector(int selectedItemIndex)
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
                    
                    // Retrieve the ID of the Inspector to be removed:
                    List<InspectorUser> inspectorUsers = (List<InspectorUser>)LVInspectorUsers.DataSource;
                    int inspectorID = inspectorUsers.ElementAt(selectedItemIndex).Inspector.InspectorID;
                    
                    // Remove the inspector and its associated AppUser from the database:
                    BLManagerDashboard.DeleteInspector(inspectorID);

                    // Update the Inspectors List Panel:
                    inspectorUsers.RemoveAll(x => (x.Inspector.InspectorID == inspectorID));
                    //LVInspectorUsers.DataSource = null;
                    LVInspectorUsers.DataBind();
                    LVSelectedInspector.DataSource = inspectorUsers;
                    LVSelectedInspector.DataBind();
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