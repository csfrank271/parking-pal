<%@ Page Title="InspectorDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULInspectorDashboard.aspx.cs" Inherits="ParkingPal.UL.ULInspectorDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true"/>
    <div class="container">
        <div class="section"> <!-- The selectable tabs at the top of the page-->
            <asp:UpdatePanel ID="UP_InspectionDashboard" runat="server" UpdateMode="Always"> 
                <ContentTemplate>
                    <div id="inspectors" class="card white">
                        <div class="card-content">
                            <div class="row">
                                <div class="col s12">
                                    <h5>Enter a registration number to view associated tickets</h5>
                                </div>
                                <div class="col s8">
                                    <asp:TextBox ID="Tbx_RegistrationNum" runat="server"></asp:TextBox>
                                    <asp:CustomValidator ID="CV_RegistrationNum" runat="server" ControlToValidate="Tbx_RegistrationNum"
                                        ErrorMessage="Registration must be between 5-7 alphanumeric characters."></asp:CustomValidator>
                                </div>
                                <div class="col s4 left-align">
                                    <asp:Button runat="server" Cssclass="btn" Text="&#xf002;" ID="BTN_SearchTickets"
                                         style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" OnClick="SearchForTickets">
                                    </asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
