<%@ Page Title="RetrieveTicketInfo" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULRetrieveTicketInfo.aspx.cs" Inherits="ParkingPal.UL.ULRetrieveTicketInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="smUpdatePanelScripts" runat="server" EnablePageMethods="true" />
    <script runat="server" EnablePartialRendering="true" id="ScriptManager1">
        protected void LV_ItemDataBound (object sender, ListViewItemEventArgs e)
        {
            var btnGetTicket = e.Item.FindControl("BTN_GetTicket") as Button;
            smUpdatePanelScripts.RegisterAsyncPostBackControl(btnGetTicket);
        }
    </script>
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <script src="https://www.paypalobjects.com/api/checkout.js"></script>
    </head>
    <div class="container">
        <div class="section">
         <!--   <asp:UpdatePanel ID="UP_RetreiveTicketInfo" runat="server" UpdateMode="Always">
                <ContentTemplate> -->
                    <div class="card">
                        <div class="card-title">Extend Parking Ticket</div>
                        <div class="card-content"> 
                            <div class="row" runat="server" id="ticketRego">
                                <div class="col s3"><b>Rego:</b></div>
                                <div class="col s3" runat="server" id="divRego"></div>
                            </div>
                            <div class="row" runat="server" id="ticketInfo">
                                <div class="col s3"><b>Start: </b></div>
                                <div runat="server" class="col s3" id="divStartTime"></div>
                                <div class="col s3"><b>End: </b></div>
                                <div runat="server" class="col s3" id="divEndTime"></div>
                            </div>
                            <div class="row col s12 m12">
                                <div class="col  s3"><b>Location: </b></div>
                                <div runat="server" class="col s3" id="divLocation"></div>
                                <div class="col s3"><b>Type: </b></div>
                                <div runat="server" class="col s3" id="divType"></div>
                            </div>
                            <div class="row col s12 m12">
                                <div class="col s6 m3"><b>Half Hourly Rate: </b></div>
                                <div class="col s6 m3">$<span runat="server" id="spanTotalPrice"></span><br /><br /></div>
                                <div class="col s12 m6 input-field"> 
                                    <asp:DropDownList runat="server" name="selectExtendTill" ID="selectExtendTill" OnSelectedIndexChanged="ticketEndTimeChanged"  AutoPostBack="true" class="material_select">
                                    </asp:DropDownList> 
                                    <label runat="server" id="labelSelectExtendTime" for="selectExtendTill">Extend Till</label>
                                </div>
                            </div> 
                            <div class="row col s12">
                                <div class="col s6 m3"><b>Total:</b></div>
                                <div class="col s6 m3">$<span id="totalCost" runat="server"></span></div>
                            </div>
                            <div class="row col s12">
                                <div class="col s6">

                                </div>
                                <div class="col s6">
                                     <div id="paypal-button"></div>
                                    <script>
                                        paypal.Button.render({

                                            env: 'sandbox', // sandbox | production
                                            client: {
                                                sandbox: 'AVPUuBdakDk-KVpbzJje3Q9LFDHLtXjFlxjszwJ28L-JrEq2flx3xVLK8Y8J-HfWyEO92yvx5ZlRcyJj',
                                                production: '<insert production client id>'
                                            },
                                            commit: true, // Show a 'Pay Now' button

                                            // payment() is called when the button is clicked
                                            payment: function (data, actions) {
                                                var total = document.getElementById('<%= totalCost.ClientID %>').innerText
                                                // Make a call to the REST api to create the payment
                                                return actions.payment.create({
                                                    payment: {
                                                        transactions: [
                                                            {
                                                                amount: { total: total, currency: 'AUD' }
                                                            }
                                                        ]
                                                    }
                                                });
                                            },

                                            // onAuthorize() is called when the buyer approves the payment
                                            onAuthorize: function (data, actions) {

                                                // Make a call to the REST api to execute the payment
                                                return actions.payment.execute().then(function () {
                                                    document.getElementById("paypal-button").hidden = true;
                                                    Materialize.toast('Payment is completed', 3000); 
                                                    window.location.href = "ULProcessExtendedTicketAndPayment.aspx";
                                                });
                                            }

                                        }, '#paypal-button');
                                    </script>

                                </div>
                            </div>
                        </div>
                    </div>
             <!--   </ContentTemplate>
            </asp:UpdatePanel>     -->   
        </div>
    </div>
</asp:Content>
 