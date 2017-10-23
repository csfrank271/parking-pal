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
            if (Convert.ToDateTime(this.labelTicketEndTime.InnerText).AddMinutes(-30) > Convert.ToDateTime(this.inputTicketStartTime.Text))
            {
                this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.labelTicketEndTime.InnerText).AddMinutes(-30).ToShortTimeString();
            } else
            {
                // show error message
            }
        }
        protected void startTimeChanged(object sender, EventArgs e)
        {
            this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.inputTicketStartTime.Text).AddMinutes(30).ToShortTimeString();
        }

       
    </script>
    <script id="jsScript">
        function enterEmailAndRegoToast() {
            // Materialize.toast(message, displayLength, className, completeCallback);
            Materialize.toast('Please enter your e-mail address and rego.', 10000); // 10000 is the duration of the toast
        }
        function regoTooLongToast() {
            Materialize.toast('Rego can not be greater than 6 characters.', 10000);
        }
        function invalidEmail() {
            Materialize.toast('Please enter a valid e-mail address.', 10000);
        }
    </script>
    <div class="container">
        <div class="card z-depth-0">
            <br />
            <div class="card-title center">
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
                        <label runat="server" id="labelUserRego" for="inputUserRego">Registration Number</label>
                    </div>
                </div>
                <div class="row col s12">
                    <div class="input-field col s4 m2">
                        <asp:textbox runat="server" AutoPostBack="true" type="text" id="inputTicketStartTime" class="timepicker" onTextChanged="startTimeChanged"/>
                        <label runat="server" id="labelTicketStartTime" for="inputTicketStartTime">Start Time</label>
                    </div>
                    <div class="col s4" style="margin-top:-3px; margin-bottom:-7px">       
                        <label runat="server" id="label1" for="inputTicketEndTime">&nbsp; &nbsp; End Time</label>
                    </div>
                    <div class="input-field col s7 m10">
                        <asp:Button id="minusTimeButton" runat="server" class="material-icons timeBtn" style="cursor: pointer;" onclick="minusTime" text="remove_circle_outline"/>&nbsp;<span runat="server" id="labelTicketEndTime" class="endTime"></span>&nbsp;<asp:Button ID="addTimeButton" runat="server" class="material-icons timeBtn" style="cursor:pointer;" OnClick="addTime" Text="add_circle_outline" />
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s12 m6">
                        <asp:DropDownList runat="server" name="parkingLotOptions" ID="parkingLotOptions" AutoPostBack="true" OnSelectedIndexChanged="parkingLotOptionChanged" class="material_select">
                            </asp:DropDownList>
                         
                        <label runat="server" id="labelSelectCarparkOptions" for="selectCarparkOptions">Carpark</label>
                    </div>
                    <div class="input-field col s12 m6">
                        <select runat="server" name="carparkTypeOptions" id="carparkTypeOptions" class="material_select"> 
                        </select>
                        <label runat="server" id="labelCarparkTypeOptions" for="selectCarparkType">Type</label>
                    </div>
                </div>
            </div>
            <div class="row col s12 m12">
                <div class="col s1 m1"></div>
                <div class="col s5 m5">
                    <asp:Button runat="server" Text="&#xf00d; &nbsp; &nbsp; Cancel" ID="BTNCancel" onclick="Cancel" style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" class="btn col s12 m6 blue accent-2"  />
                </div>  
                <div class="col s5 m5"> 
                    <asp:Button runat="server" Text="&#xf1d9; &nbsp; &nbsp; Next" ID="BTNNext" onclick="btn_Next" style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" class="btn col s12 m6"  />
                </div>
            </div>
            <div class="row col s12"></div>
        </div> 
    </div>
     
</asp:Content>