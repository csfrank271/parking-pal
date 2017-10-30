<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPages/ParkingPalMaster.Master" AutoEventWireup="true" CodeBehind="ULHome.aspx.cs" Inherits="ParkingPal.UL.ULHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/CGStyle.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" id="chart">

      // Load the Visualization API and the corechart package.
      google.charts.load('current', {'packages':['corechart']});

      // Set a callback to run when the Google Visualization API is loaded.
      //google.charts.setOnLoadCallback(drawChart);

      var chartData; // global variable for hold chart data

      // Here We will fill chartData

      $(document).ready(function () {
          $.ajax({
              url: "ULHome.aspx/getGraphData",
              data: "",
              dataType: "json",
              type: "POST",
              async: "false",
              contentType: "application/json; chartset=utf-8",
              success: function (data) {
                  chartData = data.d;
              },
              error: function () {
                  alert("Error loading data! Please try again.");
              }
          }).done(function () {
              // after complete loading data
              google.charts.setOnLoadCallback(drawChart);
              drawChart();
          });
      });

      // Callback that creates and populates a data table,
      // instantiates the pie chart, passes in the data and
      // draws it.
      function drawChart() {

        // Set chart options
        var options = {'title':'Current Car Park Capacity',
                       'width':500,
                       'height': 300,
                       vAxis: { minValue: 0, maxValue: 1, format: 'percent' }
                       };

          // Instantiate and draw our chart, passing in some options.
        console.log(chartData);
        var dataTable = new google.visualization.DataTable();
        dataTable.addColumn('string', 'Car Park');
        dataTable.addColumn('number', 'Current Usage');
        dataTable.addRows(chartData);

        var chart = new google.visualization.ColumnChart(document.getElementById('bar-graph'));
        chart.draw(dataTable, options);
      }
    </script>
    <script id="jsScript">
        function incorrectIdToast() {
            Materialize.toast('This ticket ID does not exist.', 10000);
        }
        function ticketHasExpiredToast() {
            Materialize.toast('This ticket can not be extended as it is not for today.', 10000);
        }
    </script>
    <div class="bgimg" style="background-image: url('../IMG/merewetherBathsBlocks.jpg');">
        <h5 class="home" style="font-size: 3vw;">
            ParkingPal allows people that park in ParkingPal registered parking lots to purchase online tickets, saving time and paper!
        </h5>
    </div>
    <br />
    <div class="center">
        <!--add link to purchase screen when created-->
        <div class="col row s12 m6">
            <a href="../UL/ULPurchaseTicketDashboard.aspx" class="waves-effect waves-light blue darken-3 btn-large center">
               <i class="material-icons left">
                   schedule</i>
            Buy a Ticket Now
           </a> 
        </div>
        <div class="col row s12 m6">
            <div class="col s3 m4"></div>
            <div class="input-field col s6 m2">
                <input runat="server" id="inputTicketId" type="number" class="validate" />
                <label runat="server" id="labelTicketId" for="inputTicketId">Ticket ID</label>  
            </div>
            <div class="col s12 m3">
                <asp:Button runat="server" cssClass="waves-effect waves-light blue darken-3 btn-large" onClick="extendTicket" Text="Extend Ticket" /> 
            </div>
        </div>
          
    </div>
    <br />
    <!-- if we want the collapisble table to be added dynamically we need to add the following javascript:
    $(document).ready(function(){
        $('.collapsible').collapsible();
    });
    -->
    <div id="bar-graph"></div>
    <ul class="collapsible" data-collapsible="accordion">
        <li>
            <div class="collapsible-header active center"><i class="material-icons right">expand_more</i>
                <!--this will be a dropdown of all the parking lots-->
                <h5>Select Location</h5></div>
                <div class="collapsible-body">
                    <!--the below all needs to become methods-->
                    <span>Real-time data about parking space availability: (refresh page to update data)</span>
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
