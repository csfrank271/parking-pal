<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULLogin.aspx.cs" Inherits="ParkingPal.UL.ULLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Heading / sub-heading -->
        <div class="row">
            <div class="s12">
                <h1><%: Page.Title %></h1>
            </div>
            <div class="s12">
                <h5>Single sign on for all users.</h5>
            </div>
        </div>
        
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
                    ErrorMessage="Password can not be empty.">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:button ID="btnLogin" runat="server"
                    cssclass="btn"
                    OnClick="BtnLogin_Click" Text="Submit" />
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:panel ID="pnlErrors" runat="server">
                    <p id="loginError" visible="false" runat="server">
                        Username and password combination is invalid!
                    </p>
                    <asp:CustomValidator id="cvLoginError" runat="server" 
                        Display="None" EnableClientScript="False"></asp:CustomValidator>
                </asp:panel>
            </div>
        </div>
    </div>
</asp:Content>
