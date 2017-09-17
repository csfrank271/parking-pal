using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using ParkingPal.BL;
using System.Web.Services;
using System.Text.RegularExpressions;

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
                    LVInspectorUsers.DataSource = inspectorUsers;
                    LVInspectorUsers.DataBind();
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

        // Switches the visible panel in the right-side section of the Inspector page.
        public void ChangeInspectorPanel(char panelType)
        {
            InspectorManagmentPanel_Default.Visible = false;
            InspectorManagmentPanel_Selected.Visible = false;

            switch (panelType)
            {
                case 'D':
                    InspectorManagmentPanel_Default.Visible = true;
                    break;
                case 'S':
                    InspectorManagmentPanel_Selected.Visible = true;
                    InspectorDeletionPrompt.Visible = false;
                    BTN_DeleteInspector.Visible = true;
                    break;
            }
        }

        // Attempt to update an Inspector.
        public void EditInspector(object sender, EventArgs e)
        {
            // Retrieve the ID of the Inspector to be removed:
            List<InspectorUser> inspectorUsers = (List<InspectorUser>)LVInspectorUsers.DataSource;
            int selectedItemIndex = LVInspectorUsers.SelectedIndex;
            int inspectorID = inspectorUsers.ElementAt(selectedItemIndex).Inspector.InspectorID;

            // Retrieve the user's input and set the Regex and verification checks:
            string firstName = Tbx_InspectorFirstName.Text,
                lastName = Tbx_InspectorLastName.Text;
            Regex rgxHumanName = new Regex(@"^[A-z]{1,}$");
            bool verificationPassed = true;

            // Verify first name:
            if (!rgxHumanName.IsMatch(firstName))
            {
                verificationPassed = false;
                CV_InspectorFirstName.IsValid = false;
            }
            // Verify last name:
            if (!rgxHumanName.IsMatch(lastName))
            {
                verificationPassed = false;
                CV_InspectorLastName.IsValid = false;
            }
            // Update Inspector:
            if (verificationPassed)
            {
                BLManagerDashboard.UpdateInspector(inspectorID, firstName, lastName);
            }
        }

        // Delete an Inspector.
        public void DeleteInspector(object sender, EventArgs e)
        {
            ChangeInspectorPanel('D');
            int selectedItemIndex = LVInspectorUsers.SelectedIndex;

            // Retrieve the ID of the Inspector to be removed:
            List<InspectorUser> inspectorUsers = (List<InspectorUser>)LVInspectorUsers.DataSource;
            int inspectorID = inspectorUsers.ElementAt(selectedItemIndex).Inspector.InspectorID;

            // Remove the inspector and its associated AppUser from the database:
            BLManagerDashboard.DeleteInspector(inspectorID);

            // Update the Inspectors List Panel:
            inspectorUsers.RemoveAll(x => (x.Inspector.InspectorID == inspectorID));
            LVInspectorUsers.SelectedIndex = -1;
            LVInspectorUsers.DataBind();
        }

        // Display the Inspector deletion prompt.
        public void PromptInspectorDeletion(object sender, EventArgs e)
        {
            InspectorDeletionPrompt.Visible = true;
            BTN_DeleteInspector.Visible = false;
        }

        // Remove the Inspector deletion prompt.
        public void CancelInspectorDeletion(object sender, EventArgs e)
        {
            InspectorDeletionPrompt.Visible = false;
            BTN_DeleteInspector.Visible = true;
        }
    }
}