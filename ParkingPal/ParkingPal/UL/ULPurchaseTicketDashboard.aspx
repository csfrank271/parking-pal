<%@ Page Title="PurchaseTicket" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULPurchaseTicketDashboard.aspx.cs" Inherits="ParkingPal.UL.ULPurchaseTicketDashboard" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script runat="server" EnablePartialRendering="true" id="ScriptPurchaseTicket1">
        protected void addTime(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.labelTicketEndTime.InnerText) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 24, 0, 0))
            {
                this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.labelTicketEndTime.InnerText).AddMinutes(30).ToShortTimeString();
            } else
            {
                // add code to display message that the ticket has to expire before 11:59pm or maybe set the ticket end time to 11:59pm
            }

        }
        protected void minusTime(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.labelTicketEndTime.InnerText).AddMinutes(-30) > Convert.ToDateTime(this.inputTicketStartTime.Value))
            {
                this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.labelTicketEndTime.InnerText).AddMinutes(-30).ToShortTimeString();
            } else
            {
                // show error message
            }
        }
        protected void startTimeChanged(object sender, EventArgs e)
        {
            this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.inputTicketStartTime.Value).AddMinutes(30).ToShortTimeString();
        }
    </script>
    <div class="container">
        <div class="card">
            <div class="card-title">
                Purchase Parking Ticket
            </div> 
            <div class="card-content">
                <div class="row">
                    <div class="input-field col s12 m6">
                        <input runat="server" id="inputUserEmailAddress" type="text" class="validate" /> 
                        <label runat="server" id="labelUserEmailAddress" for="inputUserEmailAddress">Email Address</label>
                    </div>
                    <div class="input-field col s12 m6">
                        <input runat="server" id="inputUserRego" type="text" class="validate" /> 
                        <label runat="server" id="labelUserRego" for="inputUserRego">Rego</label>
                    </div>
                </div>
                <div class="row col s12">
                    <div class="input-field col s4 m2">
                       <input runat="server" type="text" id="inputTicketStartTime" class="timepicker" onchange="startTimeChanged()"/>
                        <label runat="server" id="labelTicketStartTime" for="inputTicketStartTime">Start Time</label>      
                    </div> 
                    <div class="input-field col s8 m10">
                         <asp:Button id="minusTimeButton" runat="server" class="material-icons" style="cursor: pointer;" onclick="minusTime" text="remove"/>&nbsp;<span runat="server" id="labelTicketEndTime"></span>&nbsp;<asp:Button ID="addTimeButton" runat="server" class="material-icons" style="cursor:pointer;" OnClick="addTime" Text="add" />
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s12 m6">
                        <select name="carparkOptions"id="selectCarparkOptions" class="material_select">
                            <option value="ICTCarpark" selected>ICT</option>
                            <option value="EngineeringCarpark">Engineering</option>
                        </select>
                        <label runat="server" id="labelSelectCarparkOptions" for="selectCarparkOptions">Carpark</label>
                    </div>
                    <div class="input-field col s12 m6">
                        <select name="carparkTypeOptions" id="selectCarparkType" class="material_select">
                            <option value="general" selected>General</option>
                            <option value="staff">Staff</option>
                            <option value="reserved">Reserved</option>
                            <option value="position">Position</option>
                            <option value="visitors">Visitor</option>
                            <option value="carpooling">Carpooling</option>
                            <option value="disabled">Disabled</option>
                            <option value="service">Service</option>
                        </select>
                        <label runat="server" id="labelCarparkTypeOptions" for="selectCarparkType">Type</label>
                    </div>
                </div>
            </div>
            <div class="row col s12 m12">
                <div class="col s1 m1"></div>
                <div class="col s5 m5">
                <button class="btn waves-effect waves-light col s12 m6" type="reset" name="action">Cancel
                    <i class="material-icons right">close</i>
                </button>
                    </div>
                <div class="col s5 m5">
                    <asp:Button runat="server" onclick="NavigateToPayment" class="btn waves-effect waves-light col s12 m6" type="submit" name="action" text="Next" /> 
                </div>
            </div>
            <div class="row col s12"></div>
        </div> 
    </div>
     
</asp:Content>