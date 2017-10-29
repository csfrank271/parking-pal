using ParkingPal.DAL;
using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.UL
{
    public partial class ULProcessExtendedTicketAndPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewUrl = null;
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageUrl = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageUrl);
                if (redirect != null)
                {
                    strNewUrl = "~" + redirect;
                }
            }
            catch(Exception exception)
            {
                strNewUrl = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }
            finally
            {
                if (strNewUrl != null)
                {
                    Response.Redirect(strNewUrl);
                } else
                {
                    Ticket extendTicket = (Ticket)Session["ExtendTicket"];
                    TimeSpan endTime = Convert.ToDateTime(Session["EndTime"]).TimeOfDay;
                    Double paymentCost = Convert.ToDouble(Session["PaymentCost"]);
                    if (extendTicket == null || endTime == null || paymentCost == null)
                    {
                        var strUrl = "~/UL/ULHome.aspx";
                        Response.Redirect(strUrl);
                    } else
                    {
                        extendTicket.EndDateTime = new DateTime(extendTicket.EndDateTime.Year, extendTicket.EndDateTime.Month, extendTicket.EndDateTime.Day, endTime.Hours, endTime.Minutes, endTime.Seconds);
                        extendTicket.ParkingLotObject = DALPurchaseTicket.GetParkingLot(Convert.ToInt16(extendTicket.ParkingLotLocation));
                        extendTicket.ParkingBayObject = DALPurchaseTicket.GetCarparkType(Convert.ToInt16(extendTicket.CarparkType), Convert.ToInt16(extendTicket.ParkingLotLocation));
                        Payment payment = new Payment(-1, extendTicket.TicketID, (decimal)paymentCost, "Success");
                        Ticket ticket = BL.BLTicket.UpdateTicket(extendTicket);
                        int newPaymentId = BL.BLPurchaseTicket.AddPayment(payment, extendTicket); 
                        this.ticketid.InnerText = extendTicket.TicketID.ToString();
                        Session.Clear();
                    }
                }
            }
        }

    }
}