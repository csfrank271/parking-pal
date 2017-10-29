<%@ Page Title="ExtensionComplete" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULProcessExtendedTicketAndPayment.aspx.cs" Inherits="ParkingPal.UL.ULProcessExtendedTicketAndPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card col s12 z-depth-0">
            <div class="card-content">
                Your ticket has been extended successfully. Your ticket number is: <span id="ticketid" runat="server"></span>.
                <a class="" href="ULHome.aspx">Click here to return to the home page</a>
            </div>
        </div>
    </div>
</asp:Content> 
