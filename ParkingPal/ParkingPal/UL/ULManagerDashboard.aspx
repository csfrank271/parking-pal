<%@ Page Title="ManagerDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true"
    CodeBehind="ULManagerDashboard.aspx.cs" Inherits="ParkingPal.UL.ULManagerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script runat="server" EnablePartialRendering="true" ID="ScriptManager1">

    protected void LV_ItemSelected(object sender, EventArgs e)
    {
    }
    protected void LV_SelectedIndexChanging(object sender, EventArgs e)
    {
        //ListView lv = (ListView)sender;
        //lv.SelectedIndex = e.NewSelectedIndex;

    }
    protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var lbIU = e.Item.FindControl("LB_InspectorUser") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lbIU);
    }
    protected void LV_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "SelectItem":
                SelectInspector(e.Item.DataItemIndex);
                break;
            case "DeleteItem":
                DeleteInspector(LVInspectorUsers.SelectedIndex);
                break;
        }

        // Refresh the update panels:
        //UP_Inspectors.Update();
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
                                <asp:ListView runat="server" ID="LVInspectorUsers" OnItemCommand="LV_OnItemCommand"
                                    OnSelectedIndexChanging="LV_SelectedIndexChanging">
                                    <LayoutTemplate>
                                        <ul class="collection">
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_InspectorUser" class="collection-item"
                                            CommandName="SelectItem" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_InspectorUser" class="collection-item active"
                                            CommandName="SelectItem" ClientIDMode="AutoID" >
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
                                <asp:ListView runat="server" ID="LVSelectedInspector" OnItemCommand="LV_OnItemCommand"
                                    OnSelectedIndexChanging="LV_SelectedIndexChanging">
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
                                            <asp:LinkButton runat="server" ID="LB_InspectorUser"
                                                class="btn" CommandName="DeleteItem"
                                                ClientIDMode="AutoID" >
                                                <span>Delete</span>
                                            </asp:LinkButton>
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
