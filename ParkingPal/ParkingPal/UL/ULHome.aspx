<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULHome.aspx.cs" Inherits="ParkingPal.UL.ULHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="center">
        <!--add link to purchase screen when created-->
           <a class="waves-effect waves-light blue darken-3 btn-large center">
               <i class="material-icons left">
                   schedule</i>
            Buy a Ticket Now
           </a>
    </div>
    <div style="text-align:center; font-size:medium; width:">
        <br />ParkingPal allows people that park in ParkingPal registered parking lots to purchase online tickets, saving time and paper!
    </div>
    <br />
    <!-- if we want the collapisble table to be added dynamically we need to add the following javascript:
    $(document).ready(function(){
        $('.collapsible').collapsible();
    });
    -->
    <ul class="collapsible" data-collapsible="accordion">
        <li>
            <div class="collapsible-header active center"><i class="material-icons right">expand_more</i><h5>UON ICT Carpark</h5></div>
            <div class="collapsible-body">
                <span>Real-time data about parking space avilability: (refresh page to update data)</span>
                <p>% Available</p>
                <p>Free General Parking: </p>
                <p>Free Staff Parking: </p>
                <p>Free Disabled Parking: </p>
                <p>Free Carpool Parking: </p>
                <p>Free Service Parking: </p>
                <p>Free Service Parking: </p>
                <hr />
                <b>Total Available Space: </b>
            </div>
        </li>
        <li>
            <div class="collapsible-header center"><i class="material-icons right">expand_more</i><h5>Gibson St Carpark</h5></div>
            <div class="collapsible-body">
                <span>Real-time data about parking space avilability: (refresh page to update data)</span>
                <p>% Available</p>
                <p>Free General Parking: </p>
                <p>Free Staff Parking: </p>
                <p>Free Disabled Parking: </p>
                <p>Free Carpool Parking: </p>
                <p>Free Service Parking: </p>
                <p>Free Service Parking: </p>
                <hr />
                <b>Total Available Space: </b>
            </div>
        </li>
    </ul>
    <img src="../IMG/merewether baths blocks.jpg" alt="Merewether Bath Blocks" style="width:100%; position="absolute; bottom:0" />
</asp:Content>
