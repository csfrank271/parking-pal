<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULRegister.aspx.cs" Inherits="ParkingPal.UL.ULRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/CGStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bgimg" style="background-image: url('../IMG/signa 2010.jpg');">
        <h2 class="home"><%: Page.Title %></h2>
    </div>
	<!-- Form input fields -->
    <div class="container">
    <div class="card white">
        <div class="card-content">
            <div class="row">
                <h5>Enter your details below to create your free account.</h5>
				<p style="color:dimgrey"><i>Registering an account is currently available only to Management</i></p>
                <div class="input-field col m6 s12">
                    <input runat="server" id="InputEmailAddress" type="text" class="validate">
                    <label for="InputEmailAddress">Email adress</label>
                    <asp:CustomValidator ID="CV_InputEmailAddress" runat="server" ControlToValidate="InputEmailAddress"
                        ErrorMessage="The email address entered is invalid."></asp:CustomValidator>
                </div>
                <div class="input-field col m6 s12">
                    <input runat="server" id="InputFirstName" type="text" class="validate">
                    <label for="InputFirstName">First name</label>
                    <asp:CustomValidator ID="CV_InputFirstName" runat="server" ControlToValidate="InputFirstName"
                        ErrorMessage="The first name must only consist of up to 50 letters."></asp:CustomValidator>
                </div>
                <div class="input-field col m6 s12">
                    <input runat="server" id="InputLastName" type="text" class="validate">
                    <label for="InputLastName">Last name</label>
                    <asp:CustomValidator ID="CV_InputLastName" runat="server" ControlToValidate="InputLastName"
                        ErrorMessage="The last name must only consist of up to 50 letters."></asp:CustomValidator>
                </div>
                <div class="input-field col m6 s12">
                    <input runat="server" id="InputPassword" type="password" class="validate">
                    <label for="InputPassword">Password</label>
                    <asp:CustomValidator ID="CV_InputPassword" runat="server" ControlToValidate="InputPassword"
                        ErrorMessage="Password must contain between 1 - 50 characters (no spaces)."></asp:CustomValidator>
                </div>
                <div class="input-field col m6 s12">
                    <input runat="server" id="InputConfirmPassword" type="password" class="validate">
                    <label for="InputConfirmPassword">Confirm password</label>
                    <asp:CustomValidator ID="CV_InputConfirmPassword" runat="server" ControlToValidate="InputConfirmPassword"
                        ErrorMessage="Passwords must match."></asp:CustomValidator>
                </div>
                <div class="col s12">
                    <asp:Button runat="server" Cssclass="btn" Text="Register" ID="BTN_RegManager"
                        OnClick="RegisterManager" ClientIDMode="AutoID">
                    </asp:Button>
                    <asp:CustomValidator ID="CV_RegManager" runat="server"
                        ErrorMessage="Email address is already in use, please try another."></asp:CustomValidator>
                </div>
            </div>
        </div>
    </div>
    </div>
</>
</asp:Content>