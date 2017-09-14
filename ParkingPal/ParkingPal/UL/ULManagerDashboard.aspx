<%@ Page Title="ManagerDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true"
    CodeBehind="ULManagerDashboard.aspx.cs" Inherits="ParkingPal.UL.ULManagerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function show_inspector_delete_modal() {
            $('#modal_delete_inspector').modal('open');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script runat="server" EnablePartialRendering="true" ID="ScriptManager1">
    protected void LVInspectorUsers_ItemSelected(object sender, EventArgs e)
    {
    }
    protected void LVInspectorUsers_SelectedIndexChanging(object sender, EventArgs e)
    {
        //ListView lv = (ListView)sender;
        //lv.SelectedIndex = e.NewSelectedIndex;
    }
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
    protected void LVInspectorUsers_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int selectedIndex = LVInspectorUsers.SelectedIndex;
        switch(e.CommandName)
        {
            case "SelectInspector":
                SelectInspector(e.Item.DataItemIndex);
                break;
        }
    }
    protected void LVSelectedInspector_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "DeleteInspector":
                DeleteInspector(LVInspectorUsers.SelectedIndex);
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
    <div class="section">
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s4"><a href="#lots">Parking Lots</a></li>
                <li class="tab col s4"><a href="#inspectors">Inspectors</a></li>
                <li class="tab col s4"><a href="#requests">Lot Requests</a></li>
            </ul>
        </div>
    </div>
    <!-- Lots tab -->
    <div id="lots">
        <div class="card white">
            <div class="row">
                <div class="card-content">
                    <!-- Left-side content -->
                    <div class="col s12 l4">
                        <!-- Lots card -->
                        <div class="input-field">  
                            <input type="text" placeholder="&#xF002; Search parking lots"
                                style="font-family: Arial, FontAwesome"/>
                        </div>
                        <ul class="collection card">
                        </ul>
                    </div>
                    <!-- Right-side content -->
                    <div class="col s12 l8">
                        <!-- Lot Details card -->
                        <div class="card blue">
                            <div class="card-content">
                                <span class="card-title white-text">Lot Details</span>
                            </div>
                            <ul class="collection">
                                <li class="collection-item">
                                </li>
                            </ul> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Inspectors tab -->
    <div id="inspectors">
        <asp:UpdatePanel ID="UP_Inspectors" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="card white">
                    <div class="row">
                        <div class="card-content">
                            <!-- Left-side content -->
                            <div class="col s12 l4">
                                <!-- Lots card -->
                                <div class="input-field">  
                                    <input type="text" placeholder="&#xF002; Search inspectors"
                                        style="font-family: Arial, FontAwesome"/>
                                </div>
                                <asp:ListView runat="server" ID="LVInspectorUsers" OnItemCommand="LVInspectorUsers_OnItemCommand"
                                    OnSelectedIndexChanging="LVInspectorUsers_SelectedIndexChanging">
                                    <LayoutTemplate>
                                        <ul class="collection">
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item"
                                            CommandName="SelectInspector" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_SelectInspector" class="collection-item active"
                                            CommandName="SelectInspector" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="col s12">You are not managing any Inspectors.</div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                            <!-- Right-side content -->
                            <div class="col s12 l8">
                                <h3>Inspector Details</h3>
                                <!-- Inspector Details card -->
                                <asp:ListView runat="server" ID="LVSelectedInspector" OnItemCommand="LVSelectedInspector_OnItemCommand"
                                    OnSelectedIndexChanging="LVInspectorUsers_SelectedIndexChanging">
                                    <LayoutTemplate>
                                        <ul class="collection">
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="col m6 s12">
                                            <p><b>Full name: </b><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></p>
                                        </div>
                                        <div class="col m6 s12">
                                            <p><b>User name: </b><%#Eval("AppUser.UserName")%></p>
                                        </div>
                                        <div class="col m6 s12">
                                            <asp:LinkButton runat="server" ID="LB_DeleteInspector"
                                                class="btn" CommandName="ShowDeleteInspectorPrompt"
                                                ClientIDMode="AutoID" >
                                                <span>DeleteOld</span>
                                            </asp:LinkButton>
                                            <div runat="server" id="InspectorDeletionPrompt" visible="false">
                                                <p>Are you sure?</p>
                                                <asp:LinkButton runat="server" ID="LB_DeleteInspectorPromptYes"
                                                    class="btn" CommandName="DeleteInspector"
                                                    ClientIDMode="AutoID" >
                                                    <span>Yes</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="LB_DeleteInspectorPromptNo"
                                                    class="btn" CommandName="CancelInspectorDeletion"
                                                    ClientIDMode="AutoID" >
                                                    <span>No</span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p>Select an Inspector from the Inspector list to view its details here.</p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

</asp:Content>
