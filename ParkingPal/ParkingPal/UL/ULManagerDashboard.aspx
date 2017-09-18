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
        switch(e.CommandName)
        {
            case "SelectInspector":
                ChangeInspectorPanel('S');
                string[] commandArgs = e.CommandArgument.ToString().Split(',');
                Tbx_InspectorUserName.Text = commandArgs[0];
                Tbx_InspectorFirstName.Text = commandArgs[1];
                Tbx_InspectorLastName.Text = commandArgs[2];
                LVInspectorUsers.SelectedIndex = selectedIndex;
                LVInspectorUsers.DataBind();
                InspectorManagementTitle.InnerText = "Inspector Management - " + commandArgs[0];
                break;
        }
    }

    protected void LVInspectorUsers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DP_InspectorUsers.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        LVInspectorUsers.SelectedIndex = -1;
        ChangeInspectorPanel('D');
        LVInspectorUsers.DataBind();
    }
</script>
<div class="container">
    <div class="section"> <!-- The selectable tabs at the top of the page-->
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s4"><a href="#inspectors">Inspectors</a></li>
                <li class="tab col s4"><a href="#lots">Parking Lots</a></li>
                <li class="tab col s4"><a href="#requests">Lot Requests</a></li>
            </ul>
        </div>
    </div>

    <div id="lots" class="card white"> <!-- Lots tab -->
        <div class="card-content">
            <div class="row">
            </div>
        </div>
    </div>

    <!-- Inspectors tab -->
    <asp:UpdatePanel ID="UP_Inspectors" runat="server" UpdateMode="Always"> 
        <ContentTemplate>
            <div id="inspectors" class="card white">
                <div class="card-content">
                    <div class="row">
                        <div class="col s12 l4"> <!-- Left-side content (top on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5>Inspector List</h5>
                                </div>
                                <asp:ListView runat="server" ID="LVInspectorUsers" 
                                    OnItemCommand="LVInspectorUsers_OnItemCommand"
                                    OnPagePropertiesChanging="LVInspectorUsers_PagePropertiesChanging">
                                    <LayoutTemplate> <!-- This layout is shown when the Inspector list is not empty -->
                                        <div class="col s12"> <!-- The Inspector search bar -->
                                            <div class="input-field">  
                                                <input type="text" placeholder="&#xF002;  Search inspectors"
                                                    style="font-family: Arial, FontAwesome"/>
                                            </div>
                                        </div>
                                        <div class="col s12"> <!-- The Inspector list -->
                                            <div class="collection">
                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate> <!-- This layout is shown when the Inspector list is empty -->
                                        <div class="col s12">
                                            <i class="fa fa-info-circle"></i>
                                            <span>
                                                You are not managing any Inspectors. Click 'Add Inspector'
                                                in the 'Additional actions' section to add a new Inspector.
                                            </span>
                                        </div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate> <!-- This template defines the layout for an item in the Inspector list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item"
                                            CommandName="SelectInspector" ClientIDMode="AutoID"
                                            CommandArgument='<%#Eval("AppUser.UserName")+","+Eval("AppUser.FirstName")+","+
                                                Eval("AppUser.LastName")%>'>
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate> <!-- This template defines the layout for the selected item in the Inspector list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item active"
                                            CommandName="SelectInspector" ClientIDMode="AutoID"
                                            CommandArgument='<%#Eval("AppUser.UserName")+","+Eval("AppUser.FirstName")+","+
                                                Eval("AppUser.LastName")%>'>
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <div class="col s12 center-align">
                                    <asp:DataPager ID="DP_InspectorUsers" PageSize="1" PagedControlID="LVInspectorUsers" runat="server">
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
                                        <i class="fa fa-info-circle"></i>
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
                                        <p><b>First name: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorFirstName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_InspectorFirstName" runat="server" ControlToValidate="Tbx_InspectorFirstName"
                                            ErrorMessage="First name must not be blank and can only contain letters."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Last name: </b></p>
                                        <asp:TextBox ID="Tbx_InspectorLastName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_InspectorLastName" runat="server" ControlToValidate="Tbx_InspectorLastName"
                                            ErrorMessage="Last name must not be blank and can only contain letters."></asp:CustomValidator>
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
                                            ErrorMessage="User name contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>First name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorPassword" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorPassword" runat="server" ControlToValidate="Tbx_AddInspectorPassword"
                                            ErrorMessage="Password contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>First name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorFirstName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorFirstName" runat="server" ControlToValidate="Tbx_AddInspectorFirstName"
                                            ErrorMessage="First name contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>Last name: </b></p>
                                        <asp:TextBox ID="Tbx_AddInspectorLastName" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CV_AddInspectorLastName" runat="server" ControlToValidate="Tbx_AddInspectorLastName"
                                            ErrorMessage="Last name contain between 1 - 50 letters (no spaces)."></asp:CustomValidator>
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="requests" class="card white"> <!-- Lot requests tab -->
        <div class="card-content">
            <div class="row">
            </div>
        </div>
    </div>
</div>

</asp:Content>
