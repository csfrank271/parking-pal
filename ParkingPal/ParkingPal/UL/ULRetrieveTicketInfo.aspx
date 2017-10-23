<%@ Page Title="RetrieveTicketInfo" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULRetrieveTicketInfo.aspx.cs" Inherits="ParkingPal.UL.ULRetrieveTicketInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true" />
    <script runat="server" EnablePartialRendering="true" id="ScriptManager1">
        protected void LV_ItemDataBound (object sender, ListViewItemEventArgs e)
        {
            var btnGetTicket = e.Item.FindControl("BTN_GetTicket") as Button;
            smUpdatePanelScripts.RegisterAsyncPostBackControl(btnGetTicket);
        }
    </script>
    <div class="container">
        <div class="section">
            <asp:UpdatePanel ID="UP_RetreiveTicketInfo" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="card">
                        <div class="card-title">Extend Parking Ticket</div>
                        <div class="card-content">
                            <div class="row">
                               
                                <div class="col s2 m2">
                                    <asp:Button runat="server" style="font-family: FontAwesome; font-style: normal;" class="material-icons btn" OnClick="searchForTicket" text="&#xf002;" />
                                </div>
                            </div> 
                            <div class="row" runat="server" id="ticketInfo" visible="false">
                                <div class="col s12 m6 input-field">
                                    <input runat="server" id="inputTicketRego" type="text" disabled />
                                    <label runat="server" id="labelTicketRego" for="inputTicketRego">Registration Number</label>
                                </div>
                                <div class="col s12 m6 input-field">
                                    <input runat="server" id="inputRate" type="text" disabled />
                                    <label runat="server" id="labelRate" for="inputRate">Rate/30min</label>
                                </div>
                                <div class="col s12 m6 input-field">
                                    <asp:DropDownList runat="server" name="extendTicketForOptions" ID="extendTicketForOptions" AutoPostBack="true" class="material_select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>        
        </div>
    </div>
</asp:Content>
 