<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULHome.aspx.cs" Inherits="ParkingPal.UL.ULHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="image" style="padding: 50px 0px; background-position: center center; background-image: url('http://localhost:63460/IMG/merewether baths blocks.jpg'); width:100%">
        <!--<img src="../IMG/merewether baths blocks.jpg" alt="Merewether Bath Blocks" style="width:100%; position="relative" />-->
        <h4 style="text-align:center; vertical-align:central; text-shadow:2px 2px #000000; color:white;  background-color:rgba(0,0,0,0.7); padding:10px">
            <span>ParkingPal allows people that park in ParkingPal registered parking lots to purchase online tickets, saving time and paper!</span>
                </h4>
    </div>
    <br />
    <div class="center">
        <!--add link to purchase screen when created-->
           <a class="waves-effect waves-light blue darken-3 btn-large center">
               <i class="material-icons left">
                   schedule</i>
            Buy a Ticket Now
           </a>
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

</asp:Content>
