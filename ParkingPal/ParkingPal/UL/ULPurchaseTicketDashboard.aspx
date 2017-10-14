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

        protected bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

        protected void btn_Next(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.inputUserEmailAddress.Value) || string.IsNullOrWhiteSpace(this.inputUserRego.Value)) {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"jsScript","enterEmailAndRegoToast()",true);
            } else {
                if (this.inputUserRego.Value.Length > 6)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "regoTooLongToast()", true);
                }
                else
                {
                    if (!IsValidEmail(this.inputUserEmailAddress.Value))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "invalidEmail()", true);
                    } else
                    {
                        NavigateToPayment(sender, e);
                    }
                }
            }
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
                        <select runat="server" name="parkingLotOptions" id="parkingLotOptions" autoPostBack="true" onchange="handleChange()" class="material_select">
                        </select>
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
                    <asp:Button runat="server" Text="&#xf00d; &nbsp; &nbsp; Cancel" ID="BTNCancel" onclick="Cancel" style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" class="btn col s12 m6"  />
                </div>  
                <div class="col s5 m5"> 
                    <asp:Button runat="server" Text="&#xf1d9; &nbsp; &nbsp; Next" ID="BTNNext" onclick="btn_Next" style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" class="btn col s12 m6"  />
                </div>
            </div>
            <div class="row col s12"></div>
        </div> 
    </div>
     
</asp:Content>