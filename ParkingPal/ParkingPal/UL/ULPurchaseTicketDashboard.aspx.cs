using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.Models;
using ParkingPal.BL;
using System.Web.Services;

namespace ParkingPal.UL
{
    public partial class ULPurchaseTicketDashboard : System.Web.UI.Page
    { 
        string strNewURL = null;

        protected void Cancel (object sender, EventArgs e)
        {
            strNewURL = "~/UL/ULHome.aspx"; 
            Response.Redirect(strNewURL); 
        }
        protected void NavigateToPayment (object sender, EventArgs e) {
            if (String.IsNullOrWhiteSpace(this.inputUserEmailAddress.Value) 
                || String.IsNullOrWhiteSpace(this.inputUserRego.Value) 
                || String.IsNullOrWhiteSpace(this.inputTicketStartTime.Value)
                || String.IsNullOrWhiteSpace(this.labelTicketEndTime.InnerText)
                || String.IsNullOrWhiteSpace(this.carparkTypeOptions.Value)
                || String.IsNullOrWhiteSpace(this.parkingLotOptions.Value))
            {
                throw new Exception();
            } else
            {
                // make call to dl to get ticket price and rate
                var rate = BLPurchaseTicket.GetRate(Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), 6, this.carparkTypeOptions.Value);
                var duration = Convert.ToDateTime(this.labelTicketEndTime.InnerText).Subtract(Convert.ToDateTime(this.inputTicketStartTime.Value)).TotalMinutes / 30;
                var total = rate.HalfHourlyRate * (decimal)duration;
                var carpark = this.parkingLotOptions.Value;
                List<Payment> payments = new List<Payment>();
                Payment payment= new Payment(-1, -1, total); 
                payments.Add(payment);
                List<ParkingBay> parkingBays = null;
                Ticket ticket = new Ticket(-1, this.inputUserRego.Value, Convert.ToDateTime(this.inputTicketStartTime.Value), Convert.ToDateTime(this.labelTicketEndTime.InnerText), rate.HalfHourlyRate, this.parkingLotOptions.Value, this.carparkTypeOptions.Value, rate, null, null, payments);
                strNewURL = "~/UL/ULPurchaseTicketPayment.aspx";
                Session["Ticket"] = ticket;
                Response.Redirect(strNewURL);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        { 
            string strNewUrl = null;
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageURL = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageURL);
                if (redirect != null)
                {
                    strNewUrl = "~" + redirect;
                }
            }
            catch (Exception exception)
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
                    if (!this.inputTicketStartTime.Value.Any())
                    {
                        this.inputTicketStartTime.Value = DateTime.Now.ToShortTimeString();
                    }
                    if (!this.labelTicketEndTime.InnerText.Any())
                    {
                        this.labelTicketEndTime.InnerText = DateTime.Now.AddMinutes(30).ToShortTimeString();
                    }
                    if (!this.parkingLotOptions.Value.Any())
                    {
                        List<ParkingLot> parkingLots = new List<ParkingLot>();
                        parkingLots = BLPurchaseTicket.GetParkingLots();

                        this.parkingLotOptions.DataTextField = "ShortName";
                        this.parkingLotOptions.DataValueField = "ID";
                        this.parkingLotOptions.DataSource = parkingLots;
                        this.parkingLotOptions.DataBind(); 

                        if (this.parkingLotOptions.Value.Any())
                        {
                            this.carparkTypeOptions.DataTextField = "CarparkType";
                            this.carparkTypeOptions.DataValueField = "CarparkTypeID";
                            IEnumerable<ParkingLot> selectedParkingLot = parkingLots.Where(parkingLot => parkingLot.ID == Convert.ToInt16(this.parkingLotOptions.Value));
                            this.carparkTypeOptions.DataSource = selectedParkingLot.First().CarparkTypes;
                            this.carparkTypeOptions.DataBind(); 
                        }
                    }
                }
            }             
        }
    }
}