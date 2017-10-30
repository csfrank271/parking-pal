using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using ParkingPal.BL;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace ParkingPal.UL
{
    public partial class ULHome : System.Web.UI.Page
    {
        [WebMethod]
        protected void extendTicket(object sender, EventArgs e)
        {
            int ticketId = -1;
            if (this.inputTicketId.Value == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "incorrectIdToast()", true);
                // error saying a ticket id must be entered
            }
            try
            {
                ticketId = Convert.ToInt16(this.inputTicketId.Value);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "incorrectIdToast()", true);

                // throw new Exception();
                // error message
            }
            if (ticketId > 0)
            {
                var tickets = BL.BLTicket.GetTicketInfo(Convert.ToInt16(this.inputTicketId.Value));
                if (tickets.Any())
                {
                    if (tickets.FirstOrDefault().EndDateTime < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 01))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "ticketHasExpiredToast()", true);

                    }
                    else
                    {
                        Session["extendticket"] = tickets.FirstOrDefault();
                        string strNewUrl = "~/UL/ULRetrieveTicketInfo.aspx";

                        Response.Redirect(strNewUrl);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "incorrectIdToast()", true);
                    // show error message saying no ticket id
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewURL = null;

            // Authenticate the user:
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageURL = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageURL);
                if (redirect != null)
                {
                    strNewURL = "~" + redirect;
                }
                else
                {
                    // Code to run if user is authenticated.
                }
            }
            catch (Exception exception)
            {
                strNewURL = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }
            finally
            {
                // Redirect to the next page:
                if (strNewURL != null)
                {
                    Response.Redirect(strNewURL);
                } else
                {

                    
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] getGraphData()
        {
            List<ParkingLot> parkingLots = BLPurchaseTicket.GetParkingLots();
            List<ParkingLotGraph> graphData = new List<ParkingLotGraph>();
            foreach (var parkingLot in parkingLots)
            {
                int usedCarParks = BLTicket.GetUsedParkingLots(parkingLot.ID);
                int totalCarParks = BLTicket.GetTotalParkingLots(parkingLot.ID);
                decimal percentage = (decimal)usedCarParks / (decimal)totalCarParks;
                ParkingLotGraph plg = new ParkingLotGraph(parkingLot.ShortName, percentage);
                graphData.Add(plg);
            }
            var chartData = new object[graphData.Count];
            int j = 0;
            foreach ( var lot in graphData)
            {
                chartData[j] = new object[] { lot.ParkingLotName, lot.PercentageFull };
                j++;
            }
            return chartData;
        }
    }


}
