<%@ Page Title="InspectorDash" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULInspectorDashboard.aspx.cs" Inherits="ParkingPal.UL.ULInspectorDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/capture.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true"/>
<div class="container">
    <div class="section">
        <asp:UpdatePanel ID="UP_InspectionDashboard" runat="server" UpdateMode="Always"> 
            <ContentTemplate>
                <div id="inspectors" class="card white">
                    <div class="card-content">
                        <div class="row">
                            <div class="col s12">
                                <h5>Enter a registration number to view associated tickets</h5>
                            </div>
                            <div class="col s8">
                                <asp:TextBox ID="Tbx_RegistrationNum" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="CV_RegistrationNum" runat="server" ControlToValidate="Tbx_RegistrationNum"
                                    ErrorMessage="Registration must be between 5-7 alphanumeric characters."></asp:CustomValidator>
                            </div>
                            <div class="col s4 left-align">
                                <asp:Button runat="server" Cssclass="btn" Text="&#xf002;" ID="BTN_SearchTickets"
                                        style="font-family: Arial, FontAwesome" ClientIDMode="AutoID" OnClick="SearchForTickets">
                                </asp:Button>
                            </div>

                            <div class="col s12">
                                <p>
                                    Alternatively, if your device has a camera then you can simply scan the licence plate.
                                    Focus your camera on the licence plate, then select 'SCAN'. If your camera is not loading,
                                    try selecting the 'RESTART CAMERA' option. Licence plate recognition is not 100% accuracte;
                                    best results come from including only the licence plate characters in the scan.
                                </p>
                            </div>
                            <div class="col s12">
                                <button onclick="startWebcam();">RESTART CAMERA</button>
                                <asp:Button runat="server" id="BTN_Snapshot" OnClientClick="snapshot();" Text="SCAN"></asp:Button> 
                                <!--<button onclick="stopWebcam();">Stop WebCam</button>--> 
                            </div>
                            <video class="responsive-video" width=1280 height=720 id="video" controls autoplay></video>
                            <canvas  id="myCanvas" width="2560" height="1440" style="display:none"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="divider"></div>
                <div class="col s12"> <!-- Ticket listing -->
                    <h5>Tickets</h5>
                </div>
                 <div class="col s12"> <!-- Ticket listing -->
                    <p runat="server" id="regoSearch"></p>
                </div>
                <asp:ListView runat="server" ID="LVTickets">
                    <LayoutTemplate> <!-- This layout is shown when the Ticket list is not empty -->
                        <div class="col s12"> <!-- The Ticket list -->
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </div>
                    </LayoutTemplate>
                    <EmptyDataTemplate> <!-- This layout is shown when the Ticket list is empty -->
                        <div class="col s12">
                            <i class="fa fa-info-circle"></i>
                            <span>
                                There are no current tickets associated with that registration number.
                            </span>
                        </div>
                    </EmptyDataTemplate>
                    <ItemTemplate> <!-- This template defines the layout for an item in the Ticket list -->
                        <div id="ticket" class="card white">
                            <div class="card-content">
                                <div class="row">
                                    <div class="col m4 s12">
                                        <p><b>Ticket ID:</b></p>
                                        <p><%#Eval("TicketID")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Start time:</b></p>
                                        <p><%#Eval("StartDateTime")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>End time:</b></p>
                                        <p><%#Eval("EndDateTime")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Rate:</b></p>
                                        <p>$<%#Eval("Rate")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>Lot location:</b></p>
                                        <p><%#Eval("ParkingLotLocation")%></p>
                                    </div>
                                    <div class="col m4 s12">
                                        <p><b>CarparkType:</b></p>
                                        <p><%#Eval("CarParkType")%></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
</div>
</asp:Content>
