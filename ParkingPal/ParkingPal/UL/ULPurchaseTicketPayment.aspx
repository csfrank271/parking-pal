<%@ Page Title="TicketPayment" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULPurchaseTicketPayment.aspx.cs" Inherits="ParkingPal.UL.ULPurchaseTicketPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script runat="server" EnablePartialRendering="true" id="csharpscript">
            protected void PaymentCompleted()
            {
                PaymentCompleted();
            }
       </script>
    <!--fuck asp-->
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <script src="https://www.paypalobjects.com/api/checkout.js"></script>
    </head>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card">
            <div class="card-title">
                Ticket Payment - <span runat="server" id="divRego"></span>
            </div>
            <div class="card-content">
                <div runat="server" class="col s12 m6 row center" id="divTicketDate"></div>
                <div class="row col s12 m12">
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
                    <div class="col s3"><b>Total: </b></div>
                    <div class="col s3">$<span runat="server" id="spanTotalPrice"></span></div>
                </div> 
                <div class="row col s12">
                <div id="paypal-button" class="col s9 m2"></div>
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
                            var total = document.getElementById('<%= spanTotalPrice.ClientID %>').innerText
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
                                window.location.href = "ULProcessTicketAndPayment.aspx";
                            });
                        }

                    }, '#paypal-button');
                </script>
                <asp:ScriptManager ID="ScriptMgr" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                <div class="col s5 m3">
                  <asp:Button ID="BTNBack" runat="server" OnClick="BTNBack_Clicked" Text="Back" class="btn col s12 m6 blue accent-2"/>
                  <br /> &nbsp;<br />
                </div>
          </div>
    </div>
</asp:Content> 