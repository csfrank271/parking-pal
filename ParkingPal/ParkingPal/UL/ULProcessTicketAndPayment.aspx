<%@ Page Title="PurchaseComplete" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULProcessTicketAndPayment.aspx.cs" Inherits="ParkingPal.UL.ULProcessTicketAndPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card col s12">
            <div class="card-content">
                You have successful purchased ticket number <span id="ticketid" runat="server"></span>.
                <a class="" href="ULHome.aspx">Click here to return to the home page</a>
            </div>
        </div>
    </div>
</asp:Content> 