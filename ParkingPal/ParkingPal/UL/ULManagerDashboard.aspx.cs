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

                    // Initialise the list of Inspectors:
                    List<InspectorUser> inspectorUsers = BLManagerDashboard.
                        GetManagerInspectors(manager.ManagerID);
                    LVInspectorUsers.DataSource = inspectorUsers;
                    LVInspectorUsers.DataBind();

                    // Initialise the list of ParkingLots:
                    List<ParkingLot> parkingLots = BLManagerDashboard.
                        GetManagerParkingLots(manager.ManagerID);
                    LV_ParkingLots.DataSource = parkingLots;
                    LV_ParkingLots.DataBind();

                    if(!IsPostBack)
                    {
                        // Initialise the open time/close time drop down lists:
                        List<string> halfHourIntervals = BLManagerDashboard.GetHalfHourIntervalsList();
                        DDL_ParkingLotOpenTime.Items.Clear();
                        DDL_ParkingLotCloseTime.Items.Clear();
                        DDL_AddParkingLotOpenTime.Items.Clear();
                        DDL_AddParkingLotCloseTime.Items.Clear();
                        foreach (string halfHourInterval in halfHourIntervals)
                        {
                            DDL_ParkingLotOpenTime.Items.Add(new ListItem(halfHourInterval, halfHourInterval));
                            DDL_ParkingLotCloseTime.Items.Add(new ListItem(halfHourInterval, halfHourInterval));
                            DDL_AddParkingLotOpenTime.Items.Add(new ListItem(halfHourInterval, halfHourInterval));
                            DDL_AddParkingLotCloseTime.Items.Add(new ListItem(halfHourInterval, halfHourInterval));
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

        // Switches the visible panel in the right-side section of the Parking Lot page.
        [WebMethod]
        public void ChangeParkingLotPanel(char panelType)
        {
            // Default all panels:
            ParkingLotManagementPanel_Default.Visible = false;
            ParkingLotManagementPanel_Selected.Visible = false;
            ParkingLotManagementPanel_AddParkingLot.Visible = false;
            //BTN_ShowAddParkingLotPanel.Enabled = true;

            switch (panelType)
            {
                case 'D':
                    ParkingLotManagementPanel_Default.Visible = true;
                    ParkingLotManagementTitle.InnerText = "Parking Lot Management Section";
                    break;
                case 'S':
                    ParkingLotManagementPanel_Selected.Visible = true;
                    // ADD LOGIC HERE FOR CHOOSING WHICH TO DISPLAY DEPENDING ON AppprovalStatus
                    break;
                case 'A':
                    ParkingLotManagementPanel_AddParkingLot.Visible = true;
                    BTN_ShowAddParkingLotPanel.Enabled = false;
                    break;
            }
        }

        // Attempt to update a ParkingLot.
        public void EditParkingLot(object sender, EventArgs e)
        {
            // Retrieve the ID of the ParkingLot to be updated:
            List<ParkingLot> parkingLots = (List<ParkingLot>)LV_ParkingLots.DataSource;
            int selectedItemIndex = LV_ParkingLots.SelectedIndex;
            int parkingLotID = parkingLots.ElementAt(selectedItemIndex).ID;

            // Retrieve the user's input and set the Regex and verification checks:
            string name = Tbx_ParkingLotName.Text;
            string address = Tbx_ParkingLotAddress.Text;
            string coordinates = Tbx_ParkingLotCoordinates.Text;
            Regex rgxUserNamePwd = new Regex(@"^[^ ]{1,50}$");
            Regex rgxParkingLotName = new Regex(@"^[^ ]{1,}[A-z0-9 ]{1,}[^ ]{1,}$");
            Regex rgxHumanName = new Regex(@"^[A-z]{1,50}$");
            Regex rgxCoordinates = new Regex(@"^(([-]?[0-8]?[0-9])[.]([0-9]{5})|([-]?90[.][0]{5}))[,][ ](([-]?[1]?[0-7]?[0-9])[.]([0-9]{5})|([-]?180[.][0]{5}))$");
            bool verificationPassed = true;

            // Verify the ParkingLot name:
            if (!rgxParkingLotName.IsMatch(name))
            {
                verificationPassed = false;
                CV_ParkingLotName.IsValid = false;
            }
            if (!rgxParkingLotName.IsMatch(address))
            {
                verificationPassed = false;
                CV_ParkingLotAddress.IsValid = false;
            }
            if (!rgxCoordinates.IsMatch(coordinates))
            {
                verificationPassed = false;
                CV_ParkingLotCoordinates.IsValid = false;
            }
            TimeSpan openTime = TimeSpan.Parse(DDL_ParkingLotOpenTime.SelectedValue);
            TimeSpan closeTime = TimeSpan.Parse(DDL_ParkingLotCloseTime.SelectedValue);
            if(closeTime <= openTime)
            {
                verificationPassed = false;
                CV_ParkingLotCloseTime.IsValid = false;
            }
            // Update the ParkingLot:
            if (verificationPassed)
            {
                BLManagerDashboard.UpdateParkingLot(parkingLotID, name, address,
                    coordinates, openTime, closeTime);
            }
        }

        // Shows the Add Parking Lot prompt.
        public void ShowAddParkingLotPrompt(object sender, EventArgs e)
        {
            ChangeParkingLotPanel('A');
            ParkingLotManagementTitle.InnerText = "Add Parking Lot";
        }

        // Attempt to add a Parking Lot.
        public void AddParkingLot(object sender, EventArgs e)
        {
            // Retrieve the user's input and set the Regex and verification checks:
            string name = Tbx_AddParkingLotName.Text,
                address = Tbx_AddParkingLotAddress.Text,
                coordinates = Tbx_AddParkingLotCoordinates.Text;
            TimeSpan openTime = TimeSpan.Parse(DDL_AddParkingLotOpenTime.Text),
                closeTime = TimeSpan.Parse(DDL_AddParkingLotCloseTime.Text);
            Regex rgxUserNamePwd = new Regex(@"^[^ ]{1,50}$");
            Regex rgxParkingLotName = new Regex(@"^[^ ]{1,}[A-z0-9 ]{1,}[^ ]{1,}$");
            Regex rgxHumanName = new Regex(@"^[A-z]{1,50}$");
            Regex rgxCoordinates = new Regex(@"^(([-]?[0-8]?[0-9])[.]([0-9]{5})|([-]?90[.][0]{5}))[,][ ](([-]?[1]?[0-7]?[0-9])[.]([0-9]{5})|([-]?180[.][0]{5}))$");
            bool verificationPassed = true;

            // Verify the ParkingLot name:
            if (!rgxParkingLotName.IsMatch(name))
            {
                verificationPassed = false;
                CV_AddParkingLotName.IsValid = false;
            }
            if (!rgxParkingLotName.IsMatch(address))
            {
                verificationPassed = false;
                CV_AddParkingLotAddress.IsValid = false;
            }
            if (!rgxCoordinates.IsMatch(coordinates))
            {
                verificationPassed = false;
                CV_AddParkingLotCoordinates.IsValid = false;
            }

            if (closeTime <= openTime)
            {
                verificationPassed = false;
                CV_AddParkingLotCloseTime.IsValid = false;
            }
            // Update the ParkingLot:
            if (verificationPassed)
            {
                Manager manager = (Manager)Session["Manager"];
                int managerID = manager.ManagerID;
                ParkingLot parkingLot = BLManagerDashboard.AddParkingLot(managerID, name, address, coordinates, openTime, closeTime);

                ChangeParkingLotPanel('D');
                List<ParkingLot> parkingLots = (List<ParkingLot>)LV_ParkingLots.DataSource;
                if (parkingLots == null)
                {
                    parkingLots = new List<ParkingLot>();
                }
                parkingLots.Add(parkingLot);
                LV_ParkingLots.DataSource = parkingLots;
                LV_ParkingLots.DataBind();
            }
        }
    }
}