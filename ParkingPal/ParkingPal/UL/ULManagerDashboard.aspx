<%@ Page Title="ManagerDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true"
    CodeBehind="ULManagerDashboard.aspx.cs" Inherits="ParkingPal.UL.ULManagerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true"/>
<script runat="server" EnablePartialRendering="true" ID="ScriptManager1">
    // Registers controls for asynchronus postback, which allows these controls to perform partial page updates.
    protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var lbSelectParkingLot = e.Item.FindControl("LB_SelectParkingLot") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbSelectParkingLot);
        var btnEditParkingLot = e.Item.FindControl("BTN_EditParkingLot") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnEditParkingLot);
        var btnShowAddParkingLotPanel = e.Item.FindControl("BTN_ShowAddParkingLotPanel") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnShowAddParkingLotPanel);
        var btnAddParkingLot = e.Item.FindControl("BTN_AddParkingLot") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnAddParkingLot);
        var lbUpdateCarparkType = e.Item.FindControl("LB_UpdateCarparkType") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbUpdateCarparkType);
        var lbUpdateCarparkRate = e.Item.FindControl("LB_UpdateCarparkRate") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbUpdateCarparkRate);
        var btnAddParkingLotRate = e.Item.FindControl("BTN_AddParkingLotRate") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnAddParkingLotRate);
        var lbDeleteCarparkRate = e.Item.FindControl("LB_DeleteCarparkRate") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbDeleteCarparkRate);

        var lbSelectInspector = e.Item.FindControl("LB_SelectInspector") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbSelectInspector);
        var btnEditInspector = e.Item.FindControl("BTN_EditInspector") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnEditInspector);
        var btnDeleteInspector = e.Item.FindControl("BTN_DeleteInspector") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnDeleteInspector);
        var btnDeleteInspectorYes = e.Item.FindControl("BTN_DeleteInspectorYes") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnDeleteInspectorYes);
        var btnDeleteInspectorNo = e.Item.FindControl("BTN_DeleteInspectorNo") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnDeleteInspectorNo);
        var btnShowAddInspectorPanel = e.Item.FindControl("BTN_ShowAddInspectorPanel") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnShowAddInspectorPanel);
        var btnAddInspector = e.Item.FindControl("BTN_AddInspector") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnAddInspector);
    }

    // The list of commands and actions for link buttons in the 'LVInspectorUsers' list.
    protected void LVInspectorUsers_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int selectedIndex = e.Item.DisplayIndex;
        ChangeInspectorPanel('S');
        string[] commandArgs = e.CommandArgument.ToString().Split(',');
        Tbx_InspectorUserName.Text = commandArgs[0];
        Tbx_InspectorPassword.Text = commandArgs[1];
        Tbx_InspectorFirstName.Text = commandArgs[2];
        Tbx_InspectorLastName.Text = commandArgs[3];
        LVInspectorUsers.SelectedIndex = selectedIndex;
        LVInspectorUsers.DataBind();
        InspectorManagementTitle.InnerText = "Inspector Management - " + commandArgs[0];
    }

    protected void LVInspectorUsers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DP_InspectorUsers.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        LVInspectorUsers.SelectedIndex = -1;
        ChangeInspectorPanel('D');
        LVInspectorUsers.DataBind();
    }

    // The list of commands and actions for link buttons in the 'LVInspector' list.
    protected void LVParkingLots_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int selectedIndex = e.Item.DisplayIndex;
        ChangeParkingLotPanel('S');
        string[] commandArgs = e.CommandArgument.ToString().Split(',');
        Tbx_ParkingLotName.Text = commandArgs[0];
        Tbx_ParkingLotAddress.Text = commandArgs[1];
        Tbx_ParkingLotCoordinates.Text = commandArgs[2] + ',' + commandArgs[3];
        DDL_ParkingLotOpenTime.SelectedValue = DDL_AddParkingLotOpenTime.Items.FindByValue(commandArgs[4].Substring(0, 5)).Value;
        DDL_ParkingLotCloseTime.SelectedValue = DDL_AddParkingLotOpenTime.Items.FindByValue(commandArgs[5].Substring(0, 5)).Value;
        LV_ParkingLots.SelectedIndex = selectedIndex;
        LV_ParkingLots.DataBind();
        PopulateParkingLotCarparkTypesView();
        PopulateParkingLotRatesView();
        ParkingLotManagementTitle.InnerText = "Parking Lot Management - " + commandArgs[0];
    }

    protected void LVParkingLots_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DP_ParkingLots.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        LV_ParkingLots.SelectedIndex = -1;
        ChangeParkingLotPanel('D');
        LV_ParkingLots.DataBind();
    }

    // The list of commands and actions for link buttons in the 'LV_CarparkTypes' list.
    protected void LVCarparkTypes_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string commandArg = e.CommandArgument.ToString();
        TextBox tbx_ParkCount = e.Item.FindControl("TBX_ParkCount") as TextBox;

        Regex rbxParkCount = new Regex(@"^[0-9]{1,}$");
        if (!rbxParkCount.IsMatch(tbx_ParkCount.Text))
        {
            CV_CarparkTypes.IsValid = false;
        }
        else
        {
            UpdateCarparkType(commandArg, Int32.Parse(tbx_ParkCount.Text));
        }
    }

    // The list of commands and actions for link buttons in the 'LV_ParkingLotRates' list.
    protected void LVParkingLotRates_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int commandArg = Int32.Parse(e.CommandArgument.ToString());
        TextBox tbx_RateDuration = e.Item.FindControl("TBX_RateDuration") as TextBox;
        TextBox tbx_RateHourly = e.Item.FindControl("TBX_RateHourly") as TextBox;


        if(e.CommandName.Equals("UpdateCarparkRate"))
        {
            try
            {
                decimal rateDuration = Decimal.Parse(tbx_RateDuration.Text);
                decimal rateHourly = Decimal.Parse(tbx_RateHourly.Text);
                if ((rateDuration * 2) % 1 != 0 || rateDuration <= 0 || rateDuration > 24)
                {
                    CV_ParkingLotRates.Text = "Rate duration must be hourly or half hourly and be between 0.5 and 24 (e.g. 0.5, 1, 1.5, ..., 23.5, 24)";
                    CV_ParkingLotRates.IsValid = false;
                }
                else if (rateHourly <= 0 || rateHourly >= 100)
                {
                    CV_ParkingLotRates.Text = "Hourly rate must be greater than 0 and less than 100.";
                    CV_ParkingLotRates.IsValid = false;
                }
                else
                {
                    int result = UpdateRate(commandArg, rateDuration, rateHourly);

                    if(result == 0)
                    {
                        CV_ParkingLotRates.Text = "Rate duration is already in use.";
                        CV_ParkingLotRates.IsValid = false;
                    }
                }
            }
            catch (Exception exception)
            {
                CV_ParkingLotRates.Text = "Invalid input: must be a number";
                CV_ParkingLotRates.IsValid = false;
            }
        }

        else if(e.CommandName.Equals("DeleteCarparkRate"))
        {
            DeleteParkingLotRate(commandArg);
        }
    }
</script>
<div class="container">
    <div class="section"> <!-- The selectable tabs at the top of the page-->
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s4"><a href="#lots">Parking Lots</a></li>
                <li class="tab col s4"><a href="#inspectors">Inspectors</a></li>
            </ul>
        </div>
    </div>
    <!-- Lots tab -->
    <div id="lots" class="card white">
        <div class="card-content">
            <div class="row">
                <asp:UpdatePanel ID="UP_InspectorPanel" runat="server" UpdateMode="Always"> 
                    <ContentTemplate>
                        <div class="col s12 l4"> <!-- Left-side content (top on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5>Parking Lots List</h5>
                                </div>
                                <asp:ListView runat="server" ID="LV_ParkingLots" 
                                    OnItemCommand="LVParkingLots_OnItemCommand"
                                    OnPagePropertiesChanging="LVParkingLots_PagePropertiesChanging">
                                    <LayoutTemplate> <!-- This layout is shown when the ParkingLot list is not empty -->
                                        <!--<div class="col s12">
                                            <div class="input-field">  
                                                <input type="text" placeholder="&#xF002;  Search Parking Lots"
                                                    style="font-family: Arial, FontAwesome"/>
                                            </div>
                                        </div>-->
                                        <div class="col s12"> <!-- The ParkingLot list -->
                                            <div class="collection">
                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate> <!-- This layout is shown when the ParkingLot list is empty -->
                                        <div class="col s12">
                                            <i class="material-icons">info_outline</i>
                                            <span>
                                                You are not managing any Parking Lots. Click 'Request Parking Lot'
                                                in the 'Additional actions' section to request the creation of a
                                                new Parking Lot.
                                            </span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate> <!-- This template defines the layout for an item in the ParkingLot list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectParkingLot" class="collection-item"
                                            CommandName="SelectParkingLot" ClientIDMode="AutoID"
                                            CommandArgument='<%#Eval("ShortName")+","+Eval("LocationAddress")+","+
                                                Eval("Coordinates")+","+Eval("OpenTime")+","+Eval("CloseTime")%>'>
                                            <span><%#Eval("ShortName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate> <!-- This template defines the layout for the selected item in the ParkingLot list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectParkingLot" class="collection-item active grey darken-2"
                                            CommandArgument='<%#Eval("ShortName")+","+Eval("LocationAddress")+","+
                                                Eval("Coordinates")+","+Eval("OpenTime")+","+Eval("CloseTime")%>'>
                                            <span style="color:white"><%#Eval("ShortName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <div class="col s12 center-align">
                                    <asp:DataPager ID="DP_ParkingLots" PageSize="10" PagedControlID="LV_ParkingLots" runat="server">
                                        <Fields>
                                            <asp:NumericPagerField ButtonType="Link"/>
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <div class="row"> <!-- The Additional actions section for the ParkingLot list -->
                                <div class="col s12">
                                    <h5>Additional actions</h5>
                                </div>
                                <div class="col s12">
                                    <asp:Button runat="server" Cssclass="btn" Text="Add Parking Lot" ID="BTN_ShowAddParkingLotPanel"
                                        OnClick="ShowAddParkingLotPrompt" ClientIDMode="AutoID">
                                    </asp:Button> <!-- The add ParkingLot button -->
                                </div>
                            </div>
                        </div>
                        <div class="col s12 l8"> <!-- Right-side content (bottom on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5 runat="server" id="ParkingLotManagementTitle">Parking Lot Management Section</h5>
                                </div>
                                <div runat="server" id ="ParkingLotManagementPanel_Default"> <!-- The default screen for the Parking Lot Management Panel -->
                                    <div class="col s12">
                                        <i class="material-icons">info_outline</i>
                                        <span>
                                            This space is an area for managing Parking Lots. It will display details
                                            of a Parking Lot when selected from the Parking Lot list, and provide the
                                            options to edit or delete the Parking Lot in addition to assigning/
                                            revoking authority for Inspectors on the Parking Lot.
                                        </span>
                                    </div>
                                </div>
                                <div runat="server" id ="ParkingLotManagementPanel_Selected" visible="false"> <!-- The selected Parking Lot screen for the Parking Lot Management Panel -->
                                    <div class="col s12">
                                        <h5>Lot details:</h5>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Lot name: </b></p>
                                        <asp:TextBox ID="Tbx_ParkingLotName" runat="server" Enabled="true"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_ParkingLotName" runat="server" ControlToValidate="Tbx_ParkingLotName"
                                            ErrorMessage="Parking lot name must not be empty and can only include letters, numbers, and spaces."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Lot address: </b></p>
                                        <asp:TextBox ID="Tbx_ParkingLotAddress" runat="server" Enabled="true"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_ParkingLotAddress" runat="server" ControlToValidate="Tbx_ParkingLotAddress"
                                            ErrorMessage="Parking lot address must not be empty and can only include letters, numbers, and spaces."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Lot coordinates: </b></p>
                                        <asp:TextBox ID="Tbx_ParkingLotCoordinates" runat="server" Enabled="true"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_ParkingLotCoordinates" runat="server" ControlToValidate="Tbx_ParkingLotCoordinates"
                                            ErrorMessage="Parking lot coordinates must be of the following format: 'xx.xxxxx, yy.yyyyy', in order of latitude,
                                                longitude. Latitude values can range from -90 to 90. Longitude values can range from -180 to 180."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Lot open time: </b></p>
                                        <asp:DropDownList runat="server" ID="DDL_ParkingLotOpenTime" Enabled="true" style="display:inline"></asp:DropDownList>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Lot close time: </b></p>
                                        <asp:DropDownList runat="server" ID="DDL_ParkingLotCloseTime" Enabled="true" style="display:inline"></asp:DropDownList>
                                        <asp:CustomValidator ID="CV_ParkingLotCloseTime" runat="server" ControlToValidate="DDL_ParkingLotCloseTime"
                                            ErrorMessage="Close time must be after open time."></asp:CustomValidator>
                                    </div>
                                    <div class="col s12"> <!-- The edit Parking Lot button -->
                                        <asp:Button runat="server" ID="BTN_EditParkingLot" Text="Update" class="btn"
                                            ClientIDMode="AutoID" OnClick="EditParkingLot"></asp:Button>
                                    </div>
                                    <div class="col s12">
                                        <h5>Lot carparks:</h5>
                                    </div>
                                    <div class="col s12"> <!-- ParkingLot carparks -->
                                        <asp:CustomValidator ID="CV_CarparkTypes" runat="server" ErrorMessage="Spaces must be an integer greater than or equal to zero."></asp:CustomValidator>
                                        <asp:ListView ID="LV_CarparkTypes" runat="server" OnItemCommand="LVCarparkTypes_OnItemCommand">
                                            <LayoutTemplate>
                                                <table>
                                                    <thead>
                                                        <tr>
                                                            <td><asp:Label runat="server" Text="Type" /></td>
                                                            <td><asp:Label runat="server" Text="Spaces" /></td>
                                                            <td><asp:Label runat="server" Text="Update" /></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate> <!-- This layout is shown when the CarparkType list is empty -->
                                                <div class="col s12">
                                                    <i class="material-icons">info_outline</i>
                                                    <span>
                                                        There are no Carpark Types in the system.
                                                    </span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate> <!-- This template defines the layout for an item in the CarparkType list -->
                                                <tr>
                                                    <td><asp:Label runat="server" Text='<%#Eval("CarparkType")%>' /></td>
                                                    <td><asp:TextBox runat="server" Text='<%#Eval("NumberOfParks")%>' id="TBX_ParkCount" />
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="LB_UpdateCarparkType" class="btn"
                                                            Text="UPDATE" CommandName="UpdateCarparkType" ClientIDMode="AutoID"
                                                            CommandArgument='<%#Eval("CarparkType")%>'>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col s12">
                                        <h5>Parking Lot rates:</h5>
                                    </div>
                                    <div class="col s12"> <!-- ParkingLot rates -->
                                        <asp:CustomValidator ID="CV_ParkingLotRates" runat="server" ErrorMessage=""></asp:CustomValidator>
                                        <asp:ListView ID="LV_ParkingLotRates" runat="server" OnItemCommand="LVParkingLotRates_OnItemCommand">
                                            <LayoutTemplate>
                                                <table>
                                                    <thead>
                                                        <tr>
                                                            <td><asp:Label runat="server" Text="Duration Trigger" /></td>
                                                            <td><asp:Label runat="server" Text="Rate" /></td>
                                                            <td><asp:Label runat="server" Text="Actions" /></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate> <!-- This layout is shown when the ParkingLot Rate list is empty -->
                                                <div class="col s12">
                                                    <i class="material-icons">info_outline</i>
                                                    <span>
                                                        There are no rates for this ParkingLot. Customers will be charge the default rate.
                                                    </span>
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate> <!-- This template defines the layout for an item in the CarparkType list -->
                                                <tr>
                                                    <td><asp:TextBox runat="server" Text='<%#Eval("TimeDuration")%>' id="TBX_RateDuration" /></td>
                                                    <td><asp:TextBox runat="server" Text='<%#Eval("HalfHourlyRate")%>' id="TBX_RateHourly" /></td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="LB_UpdateCarparkRate" class="btn"
                                                            Text="UPDATE" CommandName="UpdateCarparkRate" ClientIDMode="AutoID"
                                                            CommandArgument='<%#Eval("RateId")%>'>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LB_DeleteCarparkRate" class="btn"
                                                            Text="DELETE" CommandName="DeleteCarparkRate" ClientIDMode="AutoID"
                                                            CommandArgument='<%#Eval("RateId")%>'>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col s12">
                                        <p>Add Rate</p>
                                        <asp:CustomValidator ID="CV_AddParkingLotRate" runat="server" ErrorMessage=""></asp:CustomValidator>
                                    </div>
                                    <div class="col s12">
                                        <div class="col s4">
                                            <asp:TextBox runat="server" Text='' id="TBX_AddRateDuration" />
                                        </div>
                                        <div class="col s4">
                                            <asp:TextBox runat="server" Text='' id="TBX_AddRateHourly" />
                                        </div>
                                        <div class="col s4">
                                            <asp:Button runat="server" Cssclass="btn" Text="Add" ID="BTN_AddParkingLotRate"
                                                OnClick="AddParkingLotRate" ClientIDMode="AutoID">
                                            </asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id ="ParkingLotManagementPanel_AddParkingLot" visible="false"> <!-- The 'Request Parking Lot' screen for the Parking Lot Management Panel -->
                                    <div class="col m6 s12">
	                                    <p><b>Lot name: </b></p>
	                                    <asp:TextBox ID="Tbx_AddParkingLotName" runat="server" Enabled="true"></asp:TextBox>
	                                    <asp:CustomValidator ID="CV_AddParkingLotName" runat="server" ControlToValidate="Tbx_AddParkingLotName"
		                                    ErrorMessage="Parking lot name must not be empty and can only include letters, numbers, and spaces."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
	                                    <p><b>Lot address: </b></p>
	                                    <asp:TextBox ID="Tbx_AddParkingLotAddress" runat="server" Enabled="true"></asp:TextBox>
	                                    <asp:CustomValidator ID="CV_AddParkingLotAddress" runat="server" ControlToValidate="Tbx_AddParkingLotAddress"
		                                    ErrorMessage="Parking lot address must not be empty and can only include letters, numbers, and spaces."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
	                                    <p><b>Lot coordinates: </b></p>
	                                    <asp:TextBox ID="Tbx_AddParkingLotCoordinates" runat="server" Enabled="true"></asp:TextBox>
	                                    <asp:CustomValidator ID="CV_AddParkingLotCoordinates" runat="server" ControlToValidate="Tbx_AddParkingLotCoordinates"
		                                    ErrorMessage="Parking lot coordinates must be of the following format: 'xx.xxxxx, yy.yyyyy', in order of latitude,
			                                    longitude. Latitude values can range from -90 to 90. Longitude values can range from -180 to 180."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
	                                    <p><b>Lot open time: </b></p>
	                                    <asp:DropDownList runat="server" ID="DDL_AddParkingLotOpenTime" Enabled="true" style="display:inline"></asp:DropDownList>
                                    </div>
                                    <div class="col m6 s12">
	                                    <p><b>Lot close time: </b></p>
	                                    <asp:DropDownList runat="server" ID="DDL_AddParkingLotCloseTime" Enabled="true" style="display:inline"></asp:DropDownList>
	                                    <asp:CustomValidator ID="CV_AddParkingLotCloseTime" runat="server" ControlToValidate="DDL_AddParkingLotCloseTime"
		                                    ErrorMessage="Close time must be after open time."></asp:CustomValidator>
                                    </div>
                                    <div class="col s12">
                                        <asp:Button runat="server" Cssclass="btn" Text="Add" ID="BTN_AddParkingLot"
                                            OnClick="AddParkingLot" ClientIDMode="AutoID">
                                        </asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </contentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
            
    <!-- Inspectors tab -->
    <div id="inspectors" class="card white">
        <div class="card-content">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always"> 
                    <ContentTemplate>
                        <div class="col s12 l4"> <!-- Left-side content (top on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5>Inspector List</h5>
                                </div>
                                <asp:ListView runat="server" ID="LVInspectorUsers" 
                                    OnItemCommand="LVInspectorUsers_OnItemCommand"
                                    OnPagePropertiesChanging="LVInspectorUsers_PagePropertiesChanging">
                                    <LayoutTemplate> <!-- This layout is shown when the Inspector list is not empty -->
                                        <!--<div class="col s12">
                                            <div class="input-field">  
                                                <input type="text" placeholder="&#xF002;  Search inspectors"
                                                    style="font-family: Arial, FontAwesome"/>
                                            </div>
                                        </div>-->
                                        <div class="col s12"> <!-- The Inspector list -->
                                            <div class="collection">
                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate> <!-- This layout is shown when the Inspector list is empty -->
                                        <div class="col s12">
                                            <i class="material-icons">info_outline</i>
                                            <span>
                                                You are not managing any Inspectors. Click 'Add Inspector'
                                                in the 'Additional actions' section to add a new Inspector.
                                            </span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate> <!-- This template defines the layout for an item in the Inspector list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item"
                                            CommandName="SelectInspector" ClientIDMode="AutoID"
                                            CommandArgument='<%#Eval("AppUser.UserName")+","+Eval("AppUser.UserPassword")+","+
                                                Eval("AppUser.FirstName")+","+Eval("AppUser.LastName")%>'>
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate> <!-- This template defines the layout for the selected item in the Inspector list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item active grey darken-2"
                                            CommandName="SelectInspector" ClientIDMode="AutoID"
                                            CommandArgument='<%#Eval("AppUser.UserName")+","+Eval("AppUser.UserPassword")+","+
                                                Eval("AppUser.FirstName")+","+Eval("AppUser.LastName")%>'>
                                            <span style="color:white"><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <div class="col s12 center-align">
                                    <asp:DataPager ID="DP_InspectorUsers" PageSize="10" PagedControlID="LVInspectorUsers" runat="server">
                                        <Fields>
                                            <asp:NumericPagerField ButtonType="Link"/>
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <div class="row"> <!-- The Additional actions section for the Inspector list -->
                                <div class="col s12">
                                    <h5>Additional actions</h5>
                                </div>
                                <div class="col s12">
                                    <asp:Button runat="server" Cssclass="btn" Text="Add Inspector" ID="BTN_ShowAddInspectorPanel"
                                        OnClick="ShowAddInspectorPrompt" ClientIDMode="AutoID">
                                    </asp:Button> <!-- The add Inspector button -->
                                </div>
                            </div>
                        </div>
                        <div class="col s12 l8"> <!-- Right-side content (bottom on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5 runat="server" id="InspectorManagementTitle">Inspector Management Section</h5>
                                </div>
                                <div runat="server" id ="InspectorManagmentPanel_Default"> <!-- The default screen for the Inspector Management Panel -->
                                    <div class="col s12">
                                        <i class="material-icons">info_outline</i>
                                        <span>
                                            This space is an area for managing Inspectors. It will display details
                                            of an Inspector when selected from the Inspector list, and provide the
                                            options to edit or delete the Inspector.
                                        </span>
                                    </div>
                                </div>
                                <div runat="server" id ="InspectorManagmentPanel_Selected" visible="false"> <!-- The selected Inspector screen for the Inspector Management Panel -->
                                    <div class="col m6 s12">
                                        <p><b>User name: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorUserName" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Password: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_InspectorPassword" runat="server" ControlToValidate="Tbx_InspectorPassword"
                                            ErrorMessage="Password must contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>First name: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorFirstName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_InspectorFirstName" runat="server" ControlToValidate="Tbx_InspectorFirstName"
                                            ErrorMessage="First name must contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Last name: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorLastName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_InspectorLastName" runat="server" ControlToValidate="Tbx_InspectorLastName"
                                            ErrorMessage="Last name must contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col s12"> <!-- The edit Inspector button -->
                                        <asp:Button runat="server" ID="BTN_EditInspector" Text="Update" class="btn"
                                            ClientIDMode="AutoID" OnClick="EditInspector"></asp:Button>
                                    </div>
                                    <div class="col s12"> <!-- The Inspector delete button -->
                                        <asp:Button runat="server" ID="BTN_DeleteInspector" Text="Delete" class="btn"
                                            ClientIDMode="AutoID" OnClick="PromptInspectorDeletion"></asp:Button>
                                    </div>
                                    <div class="col s12" runat="server" id="InspectorDeletionPrompt" visible="false"> <!-- Inspector deletion confirmation prompt -->
                                        <div class="col s12">
                                            <p>Are you sure?</p>
                                        </div>
                                        <div class="col s6">
                                            <asp:Button runat="server" ID="BTN_DeleteInspectorYes" Text="Yes" class="btn"
                                                ClientIDMode="AutoID" OnClick="DeleteInspector"></asp:Button>
                                        </div>
                                        <div class="col s6">
                                            <asp:Button runat="server" ID="BTN_DeleteInspectorNo" Text="No" class="btn"
                                                ClientIDMode="AutoID" OnClick="CancelInspectorDeletion"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id ="InspectorManagmentPanel_AddInspector" visible="false"> <!-- The 'Add Inspector' screen for the Inspector Management Panel -->
                                    <div class="col m6 s12">
                                        <p><b>User name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorUserName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorUserName" runat="server" ControlToValidate="Tbx_AddInspectorUserName"
                                            ErrorMessage="User name must contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Password: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorPassword" runat="server" ControlToValidate="Tbx_AddInspectorPassword"
                                            ErrorMessage="Password must contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>First name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorFirstName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorFirstName" runat="server" ControlToValidate="Tbx_AddInspectorFirstName"
                                            ErrorMessage="First name must contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Last name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorLastName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorLastName" runat="server" ControlToValidate="Tbx_AddInspectorLastName"
                                            ErrorMessage="Last name must contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col s12">
                                        <asp:Button runat="server" Cssclass="btn" Text="Add" ID="BTN_AddInspector"
                                            OnClick="AddInspector" ClientIDMode="AutoID">
                                        </asp:Button>
                                        <asp:CustomValidator ID="CV_AddInspectorExists" runat="server"
                                            ErrorMessage="User name is in use, please try another."></asp:CustomValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
</asp:Content>
