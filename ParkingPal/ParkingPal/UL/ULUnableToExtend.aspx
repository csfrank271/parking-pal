<%@ Page Language="C#"  MasterPageFile="~/MasterPages/ParkingPalMaster.Master"  AutoEventWireup="true" CodeBehind="ULUnableToExtend.aspx.cs" Inherits="ParkingPal.UL.ULUnableToExtend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <p>
                        Your ticket is unable to be extended as it goes passed the parking lot closing time. <a class="" href="ULHome.aspx">Click here to return to the home page</a>
                    </p> 
                </div>
            </div>
        </div>
    </div>
</asp:Content>