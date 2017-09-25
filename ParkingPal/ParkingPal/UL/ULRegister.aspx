<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULRegister.aspx.cs" Inherits="ParkingPal.UL.ULRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/CGStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Heading / sub-heading -->
        <div class="bgimg" style="background-image: url('../IMG/signa 2010.jpg');">
            <h2 class="home"><%: Page.Title %></h2>
        </div>
    <!-- Form input fields -->
        <div class="row">
            <div class="input-field col m6 s12">
                <input runat="server" id="inputName" type="text" class="validate">
                <label for="inputUserName">User Name</label>
               <!-- <asp:RequiredFieldValidator
                    ID="rfvUserName"
                    ControlToValidate="inputUserName"
                    Display="Dynamic"
                    runat="server"
                    ErrorMessage="User name can not be empty.">
                </asp:RequiredFieldValidator> -->
            </div>
            <div class="input-field col m6 s12">
                <input runat="server" id="inputPassword" type="password" class="validate">
                <label for="inputPassword">Password</label>
                <!-- <asp:RequiredFieldValidator
                    ID="rfvPassword"
                    ControlToValidate="inputPassword"
                    Display="Dynamic"
                    runat="server"
                    ErrorMessage="Password can not be empty.">
                </asp:RequiredFieldValidator> -->
            </div>
        </div>
</asp:Content>