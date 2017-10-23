<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULLogin.aspx.cs" Inherits="ParkingPal.UL.ULLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/CGStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Heading / sub-heading -->
        <div class="bgimg" style="background-image: url('../IMG/signa 2010.jpg');">
            <h2 class="home"><%: Page.Title %></h2>
        </div>
        <div class="s12 container">
            <h5>Single sign on for all users.</h5>
            <p style="color:dimgrey"><i>Login is currently available only to Inspectors and Management</i></p>
        </div>
    <div class="container">        
        <!-- Form input fields -->
        <div class="row">
            <div class="input-field col m6 s12">
                <input runat="server" id="inputUserName" type="text" class="validate">
                <label for="inputUserName">User Name</label>
                <asp:RequiredFieldValidator
                    ID="rfvUserName"
                    ControlToValidate="inputUserName"
                    Display="Dynamic"
                    runat="server"
                    Style="color:red"
                    ErrorMessage="User name can not be empty.">
                </asp:RequiredFieldValidator>
            </div>
            <div class="input-field col m6 s12">
                <input runat="server" id="inputPassword" type="password" class="validate">
                <label for="inputPassword">Password</label>
                <asp:RequiredFieldValidator
                    ID="rfvPassword"
                    ControlToValidate="inputPassword"
                    Display="Dynamic"
                    runat="server"
                    Style="color:red"
                    ErrorMessage="Password can not be empty.">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:button ID="btnLogin" runat="server"
                    cssclass="btn blue darken-3"
                    OnClick="BtnLogin_Click" Text="Submit" />
            </div>
            <br />
            <div class="col s12">
               <a href="../UL/ULRegister.aspx">
                Want to register your parking lot to ParkingPal? Create a free account here.
               </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:panel ID="pnlErrors" runat="server">
                    <p id="loginError" visible="false" runat="server">
                        Username and password combination is invalid!
                    </p>
                </asp:panel>
            </div>
        </div>
    </div>
</asp:Content>
