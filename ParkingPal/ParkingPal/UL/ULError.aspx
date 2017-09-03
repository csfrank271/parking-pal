<%@ Page Title="Error" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULError.aspx.cs" Inherits="ParkingPal.UL.ULError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <p>
                        An unexpected error has occured with ParkingPal. We apologise for any inconvenience this error has caused. Please send us the following information
                        so we can fix the problem:
                    </p>
                    <code id="exceptionMsg" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
