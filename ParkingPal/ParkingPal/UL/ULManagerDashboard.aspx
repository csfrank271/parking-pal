<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULManagerDashboard.aspx.cs" Inherits="ParkingPal.UL.ULManagerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div class="section">
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s4"><a href="#lots">Parking Lots</a></li>
                <li class="tab col s4"><a href="#inspectors">Inspectors</a></li>
                <li class="tab col s4"><a href="#requests">Lot Requests</a></li>
            </ul>
        </div>
    </div>
    <!-- Lots tab -->
    <div id="lots">
        <div class="card white">
            <div class="row">
                <div class="card-content">
                    <!-- Left-side content -->
                    <div class="col s12 l4">
                        <!-- Lots card -->
                        <div class="input-field">  
                            <input type="text" placeholder="&#xF002; Search parking lots"
                                style="font-family: Arial, FontAwesome"/>
                        </div>
                        <ul class="collection card">
                            <li *ngFor="let parkingLot of parkingLots" class="collection-item"
                                [class.active]="parkingLot === selectedParkingLot"
                                (click)="onSelect(parkingLot)">
                                <span>{{parkingLot.lotName}}</span>
                            </li>
                        </ul>
                    </div>
                    <!-- Right-side content -->
                    <div class="col s12 l8">
                        <!-- Lot Details card -->
                        <div class="card blue">
                            <div class="card-content">
                                <span class="card-title white-text">Lot Details</span>
                            </div>
                            <ul class="collection">
                                <li class="collection-item">
                                    <parking-lot-detail [parkingLot]="selectedParkingLot"></parking-lot-detail>
                                </li>
                            </ul> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Inspectors tab -->
    <div id="inspectors">
        <div class="card white">
            <div class="row">
                <div class="card-content">
                    <!-- Left-side content -->
                    <div class="col s12 l4">
                        <!-- Lots card -->
                        <div class="input-field">  
                            <input type="text" placeholder="&#xF002; Search inspectors"
                                style="font-family: Arial, FontAwesome"/>
                        </div>
                        <ul class="collection card">
                            <!--<li *ngFor="let inspector of inspectors class="collection-item"
                                [class.active]="inspector === selectedInspector"
                                (click)="onSelect(inspector)">
                                <span>{{inspector.name}}</span>
                            </li>-->
                            <li class="collection-item">
                                Kyle Hayes
                            </li>
                        </ul>
                    </div>
                    <!-- Right-side content -->
                    <div class="col s12 l8">
                        <!-- Lot Details card -->
                        <div class="card blue">
                            <div class="card-content">
                                <span class="card-title white-text">Inspector Details</span>
                            </div>
                            <ul class="collection">
                                <li class="collection-item">
                                    <!--<inspector-detail [inspector]="selectedInspector"></inspector-detail>-->
                                    <span>Test</span>
                                </li>
                            </ul> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
