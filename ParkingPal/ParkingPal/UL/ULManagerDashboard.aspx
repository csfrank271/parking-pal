<%@ Page Title="ManagerDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true"
    CodeBehind="ULManagerDashboard.aspx.cs" Inherits="ParkingPal.UL.ULManagerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script runat="server" EnablePartialRendering="true" ID="ScriptManager1">
    
    // Registers controls for asynchronus postback, which allows these controls to perform partial page updates.
    protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var lbSelectInspector = e.Item.FindControl("LB_SelectInspector") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbSelectInspector);
        var lbDeleteInspector = e.Item.FindControl("LB_DeleteInspector") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbDeleteInspector);
        var lbDeleteInspectorPromptYes = e.Item.FindControl("LB_DeleteInspectorPromptYes") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbDeleteInspector);
        var lbDeleteInspectorPromptNo = e.Item.FindControl("LB_DeleteInspectorPromptNo") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbDeleteInspector);
    }
    
    // The list of commands and actions for link buttons in the 'LVInspectorUsers' list.
    protected void LVInspectorUsers_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int selectedIndex = LVInspectorUsers.SelectedIndex;
        switch(e.CommandName)
        {
            case "SelectInspector":
                SelectInspector(e.Item.DataItemIndex);
                InspectorManagmentPanel_Default.Visible = false;
                InspectorManagmentPanel_Selected.Visible = true;
                break;
        }
    }

    // The list of commands and actions for link buttons in the 'LVSelectedInspector' list.
    protected void LVSelectedInspector_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "DeleteInspector":
                DeleteInspector(LVInspectorUsers.SelectedIndex);
                InspectorManagmentPanel_Default.Visible = true;
                InspectorManagmentPanel_Selected.Visible = false;
                break;
            case "ShowDeleteInspectorPrompt":
                LVSelectedInspector.Items[0].FindControl("LB_DeleteInspector").Visible = false;
                LVSelectedInspector.Items[0].FindControl("InspectorDeletionPrompt").Visible = true;
                break;
            case "CancelInspectorDeletion":
                LVSelectedInspector.Items[0].FindControl("LB_DeleteInspector").Visible = true;
                LVSelectedInspector.Items[0].FindControl("InspectorDeletionPrompt").Visible = false;
                break;
        }
    }
</script>
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" />
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
                                <asp:ListView runat="server" ID="LVInspectorUsers" OnItemCommand="LVInspectorUsers_OnItemCommand">
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
                                            CommandName="SelectInspector" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate> <!-- This template defines the layout for the selected item in the Inspector list -->
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item active"
                                            CommandName="SelectInspector" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="divider"></div>
                            <div class="row"> <!-- The Additional actions section for the Inspector list -->
                                <div class="col s12">
                                    <h5>Additional actions</h5>
                                </div>
                                <div class="col s12">
                                    <asp:LinkButton runat="server" ID="LB_AddInspector" class="btn"
                                        CommandName="AddInspector" ClientIDMode="AutoID" >
                                        <span>Add Inspector</span>  <i class="fa fa-plus-square"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col s12 l8"> <!-- Right-side content (bottom on small screens) -->
                            <div class="row">
                                <div class="col s12">
                                    <h5>Inspector Management</h5>
                                </div>
                                <div runat="server" id ="InspectorManagmentPanel_Default"> <!-- The default screen for the Inspector Management Panel -->
                                    <div class="col s12">
                                        <i class="fa fa-info-circle"></i>
                                        <span>
                                            This space is an area for managing Inspectors. It will display details
                                            of an Inspector when selected from the Inspector list, and provide the
                                            options to edit or delete the Inspector.
                                            This space will also provide the form for creating a new Inspector by
                                            selecting the 'Add Inspector' button in the 'Additional actions' section.
                                        </span>
                                    </div>
                                </div>
                                <div runat="server" id ="InspectorManagmentPanel_Selected" visible="false"> <!-- The selected Inspector screen for the Inspector Management Panel -->
                                    <asp:ListView runat="server" ID="LVSelectedInspector" OnItemCommand="LVSelectedInspector_OnItemCommand">
                                        <LayoutTemplate> <!-- This layout is shown when an Inspector is selected -->
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                        </LayoutTemplate>
                                        <ItemTemplate> <!-- This template defines the layout for the selected Inspector details and options -->
                                            <div class="col s12">
                                                <p><b>Full name: </b><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></p>
                                            </div>
                                            <div class="col s12">
                                                <p><b>User name: </b><%#Eval("AppUser.UserName")%></p>
                                            </div>
                                            <div class="col s12"> <!-- The Inspector delete button -->
                                                <asp:LinkButton runat="server" ID="LB_DeleteInspector"
                                                    class="btn" CommandName="ShowDeleteInspectorPrompt"
                                                    ClientIDMode="AutoID" >
                                                    <span>Delete Inspector</span>
                                                </asp:LinkButton>
                                            </div>
                                            <div runat="server" id="InspectorDeletionPrompt" visible="false"> <!-- Inspector deletion confirmation prompt -->
                                                <div class="col s12">
                                                    <p>Are you sure?</p>
                                                </div>
                                                <div class="col s12">
                                                    <asp:LinkButton runat="server" ID="LB_DeleteInspectorPromptYes" class="btn"
                                                        CommandName="DeleteInspector" ClientIDMode="AutoID" >
                                                        <span>Yes</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col s12">
                                                    <asp:LinkButton runat="server" ID="LB_DeleteInspectorPromptNo" class="btn"
                                                        CommandName="CancelInspectorDeletion" ClientIDMode="AutoID" >
                                                        <span>No</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
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
