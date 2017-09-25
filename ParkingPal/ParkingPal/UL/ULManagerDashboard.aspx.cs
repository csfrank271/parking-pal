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
        [WebMethod]
        public void ChangeInspectorPanel(char panelType)
        {
            // Default all panels:
            InspectorManagmentPanel_Default.Visible = false;
            InspectorManagmentPanel_Selected.Visible = false;
            InspectorManagmentPanel_AddInspector.Visible = false;
            BTN_ShowAddInspectorPanel.Enabled = true;

            switch (panelType)
            {
                case 'D':
                    InspectorManagmentPanel_Default.Visible = true;
                    InspectorManagementTitle.InnerText = "Inspector Management Section";
                    break;
                case 'S':
                    InspectorManagmentPanel_Selected.Visible = true;
                    InspectorDeletionPrompt.Visible = false;
                    BTN_DeleteInspector.Visible = true;
                    break;
                case 'A':
                    InspectorManagmentPanel_AddInspector.Visible = true;
                    BTN_ShowAddInspectorPanel.Enabled = false;
                    Tbx_AddInspectorFirstName.Text = "";
                    Tbx_AddInspectorLastName.Text = "";
                    Tbx_AddInspectorPassword.Text = "";
                    Tbx_AddInspectorUserName.Text = "";
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
            string password = Tbx_InspectorPassword.Text, 
                firstName = Tbx_InspectorFirstName.Text,
                lastName = Tbx_InspectorLastName.Text;
            Regex rgxUserNamePwd = new Regex(@"^[^ ]{1,50}$");
            Regex rgxHumanName = new Regex(@"^[A-z]{1,50}$");
            bool verificationPassed = true;

            // Verify password:
            if (!rgxUserNamePwd.IsMatch(password))
            {
                verificationPassed = false;
                CV_InspectorPassword.IsValid = false;
            }
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
                BLManagerDashboard.UpdateInspector(inspectorID, password, firstName, lastName);
            }
        }

        // Delete an Inspector.
        public void DeleteInspector(object sender, EventArgs e)
        {
            ChangeInspectorPanel('D');
            int selectedItemIndex = LVInspectorUsers.SelectedIndex;
            int actuaItemPosition = DP_InspectorUsers.StartRowIndex + selectedItemIndex;

            // Retrieve the ID of the Inspector to be removed:
            List<InspectorUser> inspectorUsers = (List<InspectorUser>)LVInspectorUsers.DataSource;
            int inspectorID = inspectorUsers.ElementAt(actuaItemPosition).Inspector.InspectorID;

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

        // Shows the Add Inspector prompt.
        public void ShowAddInspectorPrompt(object sender, EventArgs e)
        {
            ChangeInspectorPanel('A');
            InspectorManagementTitle.InnerText = "Add Inspector";
        }

        // Attempt to add an Inspector.
        public void AddInspector(object sender, EventArgs e)
        {
            CV_AddInspectorExists.IsValid = true;
            // Retrieve the user's input and set the Regex and verification checks:
            string userName = Tbx_AddInspectorUserName.Text,
                password = Tbx_AddInspectorPassword.Text,
                firstName = Tbx_AddInspectorFirstName.Text,
                lastName = Tbx_AddInspectorLastName.Text;
            Regex rgxUserNamePwd = new Regex(@"^[^ ]{1,50}$");
            Regex rgxHumanName = new Regex(@"^[A-z]{1,50}$");
            bool verificationPassed = true;
            
            // Verify user name:
            if (!rgxUserNamePwd.IsMatch(userName))
            {
                verificationPassed = false;
                CV_AddInspectorUserName.IsValid = false;
            }
            // Verify password:
            if (!rgxUserNamePwd.IsMatch(password))
            {
                verificationPassed = false;
                CV_AddInspectorPassword.IsValid = false;
            }
            // Verify first name:
            if (!rgxHumanName.IsMatch(firstName))
            {
                verificationPassed = false;
                CV_AddInspectorFirstName.IsValid = false;
            }
            // Verify last name:
            if (!rgxHumanName.IsMatch(lastName))
            {
                verificationPassed = false;
                CV_AddInspectorLastName.IsValid = false;
            }
            // Update Inspector:
            if (verificationPassed)
            {
                Manager manager = (Manager)Session["Manager"];
                int managerID = manager.ManagerID;
                InspectorUser inspectorUser = BLManagerDashboard.AddInspector(managerID, userName,
                    password, firstName, lastName);
                if(inspectorUser == null)
                {
                    CV_AddInspectorExists.IsValid = false;
                }
                else
                {
                    // Actions to occur if InspectorUser has been successfully added:
                    ChangeInspectorPanel('D');
                    List<InspectorUser> inspectorUsers = (List<InspectorUser>)LVInspectorUsers.DataSource;
                    if(inspectorUsers == null)
                    {
                        inspectorUsers = new List<InspectorUser>();
                    }
                    inspectorUsers.Add(inspectorUser);
                    LVInspectorUsers.DataSource = inspectorUsers;
                    LVInspectorUsers.DataBind();
                }
            }
        }
    }
}