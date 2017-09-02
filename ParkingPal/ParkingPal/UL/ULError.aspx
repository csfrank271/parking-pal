<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULError.aspx.cs" Inherits="ParkingPal.UL.ULError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Unfortunately an error has occured. Our system has generated the following error message for you:
    </p>
    <p><code id="exceptionMsg" runat="server"></code></p>
    <p>
        If you don't understand this message, then the error is likely our fault. There is no reason
        for you to worry. Please send the error message to us via an email and we will inspect the
        issue.
    </p>
</asp:Content>
