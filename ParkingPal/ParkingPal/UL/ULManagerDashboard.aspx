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
        var lb = e.Item.FindControl("LB_InspectorUser") as LinkButton;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(lb);
    }
    protected void LV_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        UpdateInspectorsList(dataItem);
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
                        <asp:UpdatePanel ID="UP_InspectorUsers" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:ListView runat="server" ID="LVInspectorUsers" OnItemCommand="LV_OnItemCommand"
                                    OnSelectedIndexChanging="LV_SelectedIndexChanging">
                                    <LayoutTemplate>
                                        <ul class="collection">
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                            <p runat="server" id="test"></p>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_InspectorUser" class="collection-item"
                                            CommandName="Select" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <asp:LinkButton runat="server" ID="LB_InspectorUser" class="collection-item active"
                                            CommandName="Select" ClientIDMode="AutoID" >
                                            <span><%#Eval("AppUser.FirstName")%>&nbsp;<%#Eval("AppUser.LastName")%></span>
                                        </asp:LinkButton>
                                    </SelectedItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="col s12">You are not managing any Inspectors.</div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- Right-side content -->
                    <div class="col s12 l8">
                        <!-- Lot Details card -->
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <h3>Inspector Details</h3>
                                <div id="SelectedInspector_Unchosen">
                                    <span>Select an Inspector to view its details here.</span>
                                </div>
                                <div id="SelectedInspector_Chosen" visible="false" class="row">
                                    <div class="col m6 s12">
                                        <p><b>Full name: </b><span id="InspectorsFullName" runat="server"></span></p>
                                    </div>
                                    <div class="col m6 s12">
                                        <p><b>User name: </b><span id="InspectorsUserName" runat="server"></span></p>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
