<%@ Page Title="InspectorDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULInspectorDashboard.aspx.cs" Inherits="ParkingPal.UL.ULInspectorDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true"/>
<script runat="server" EnablePartialRendering="true" ID="ScriptManager1">
    // Registers controls for asynchronus postback, which allows these controls to perform partial page updates.
    protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var btnSearchTickets = e.Item.FindControl("BTN_SearchTickets") as Button;
        smUpdatePanelScripts.RegisterAsyncPostBackControl(btnSearchTickets);
    }
</script>
<div class="container">
    <div class="section">
        <asp:UpdatePanel ID="UP_InspectionDashboard" runat="server" UpdateMode="Always"> 
            <ContentTemplate>
                <div id="inspectors" class="card grey darken-1" style="position: sticky; right: 0; left: 0;">
                    <div class="card-content">
                        <div class="row">
                            <div class="col s12">
                                <h5 style="color:white">Enter a registration number to view associated tickets</h5>
                            </div>
                            <div class="col s8">
                                <asp:TextBox ID="Tbx_RegistrationNum" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="CV_RegistrationNum" runat="server" ControlToValidate="Tbx_RegistrationNum"
                                    ErrorMessage="Registration must be between 5-7 alphanumeric characters."></asp:CustomValidator>
                            </div>
                            <div class="col s4 left-align">
                                <asp:Button runat="server" Cssclass="waves-effect waves-light blue darken-3 btn center" Text="&#xf002;" ID="BTN_SearchTickets"
                                        style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" OnClick="SearchForTickets">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="divider"></div>
                <div class="col s12"> <!-- Ticket listing -->
                    <h5>Tickets</h5>
                </div>
                <div class="divider"></div>
                <asp:ListView runat="server" ID="LVTickets">
                    <LayoutTemplate> <!-- This layout is shown when the Ticket list is not empty -->
                        <div class="col s12"> <!-- The Ticket list -->
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </div>
                    </LayoutTemplate>
                    <EmptyDataTemplate> <!-- This layout is shown when the Ticket list is empty -->
                        <div class="col s12">
                            <i class="fa fa-info-circle"></i>
                            <span>
                                There are no current tickets associated with that registration number.
                            </span>
                        </div>
                    </EmptyDataTemplate>
                    <ItemTemplate> <!-- This template defines the layout for an item in the Ticket list -->
                        <div id="ticket" class="card white">
                            <div class="card-content">
                                <div class="row">
                                    <div class="col m4 s12">
                                        <p><b>Ticket ID:</b></p>
                                        <p><%#Eval("TicketID")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Start time:</b></p>
                                        <p><%#Eval("StartDateTime")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>End time:</b></p>
                                        <p><%#Eval("EndDateTime")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Rate:</b></p>
                                        <p>$<%#Eval("Rate")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Lot location:</b></p>
                                        <p><%#Eval("ParkingLotLocation")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>CarparkType:</b></p>
                                        <p><%#Eval("CarParkType")%></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>
