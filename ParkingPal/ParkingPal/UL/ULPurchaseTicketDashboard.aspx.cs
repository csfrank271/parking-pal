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
        List<ParkingLot> parkingLots;

        [WebMethod]
        protected void parkingLotOptionChanged(object sender, EventArgs e)
        {
            var test = this.parkingLotOptions.SelectedValue;
            this.parkingLots = BL.BLPurchaseTicket.GetParkingLots();
            IEnumerable<ParkingLot> parkinglot = this.parkingLots.Where(parkingLot => parkingLot.ID == Convert.ToInt16(this.parkingLotOptions.SelectedValue));
            this.carparkTypeOptions.DataSource = parkinglot.First().CarparkTypes;
            this.carparkTypeOptions.DataBind();
            this.carparkTypeOptions.SelectedIndex = 0;
        }

        protected bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btn_Next(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.inputUserEmailAddress.Value) || string.IsNullOrWhiteSpace(this.inputUserRego.Value))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "enterEmailAndRegoToast()", true);
            }
            else
            {
                if (this.inputUserRego.Value.Length > 6)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "regoTooLongToast()", true);
                }
                else
                {
                    if (!IsValidEmail(this.inputUserEmailAddress.Value))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsScript", "invalidEmail()", true);
                    }
                    else
                    {
                        NavigateToPayment(sender, e);
                    }
                }
            }
        }
        protected void Cancel (object sender, EventArgs e)
        {
            strNewURL = "~/UL/ULHome.aspx"; 
            Response.Redirect(strNewURL); 
        }
        protected void NavigateToPayment (object sender, EventArgs e) {
            if (String.IsNullOrWhiteSpace(this.inputUserEmailAddress.Value) 
                || String.IsNullOrWhiteSpace(this.inputUserRego.Value) 
                || String.IsNullOrWhiteSpace(this.inputTicketStartTime.Text)
                || String.IsNullOrWhiteSpace(this.labelTicketEndTime.InnerText)
                || String.IsNullOrWhiteSpace(this.carparkTypeOptions.Value)
                || String.IsNullOrWhiteSpace(this.parkingLotOptions.SelectedValue))
            {
                throw new Exception();
            } else
            {
                // make call to dl to get ticket price and rate
                var rate = BLPurchaseTicket.GetRate(Convert.ToDateTime(this.inputTicketStartTime.Text), Convert.ToDateTime(this.labelTicketEndTime.InnerText), Convert.ToInt16(this.carparkTypeOptions.Value), this.carparkTypeOptions.Value);
                var duration = Convert.ToDateTime(this.labelTicketEndTime.InnerText).Subtract(Convert.ToDateTime(this.inputTicketStartTime.Text)).TotalMinutes / 30;
                var total = rate.HalfHourlyRate * (decimal)duration;
                var carpark = this.parkingLotOptions.SelectedValue;
                List<Payment> payments = new List<Payment>();
                Payment payment= new Payment(-1, -1, total, "unconfirmed"); 
                payments.Add(payment);
                List<ParkingBay> parkingBays = null; 

                Ticket ticket = new Ticket(-1, this.inputUserRego.Value, Convert.ToDateTime(this.inputTicketStartTime.Text), Convert.ToDateTime(this.labelTicketEndTime.InnerText), rate.HalfHourlyRate, this.parkingLotOptions.SelectedValue, this.carparkTypeOptions.Value, rate, null, null, payments, this.inputUserEmailAddress.Value);
                strNewURL = "~/UL/ULPurchaseTicketPayment.aspx";
                Session["Ticket"] = ticket;
                Response.Redirect(strNewURL);
            }
        }

        [WebMethod]
        protected void startTimeChanged(object sender, EventArgs e)
        {
            this.labelTicketEndTime.InnerText = Convert.ToDateTime(this.inputTicketStartTime.Text).AddMinutes(30).ToShortTimeString();
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
                } else if ( !IsPostBack )
                {
                    if (!this.inputTicketStartTime.Text.Any())
                    {
                        this.inputTicketStartTime.Text = DateTime.Now.ToShortTimeString();
                    }
                    if (!this.labelTicketEndTime.InnerText.Any())
                    {
                        this.labelTicketEndTime.InnerText = DateTime.Now.AddMinutes(30).ToShortTimeString();
                    }
                    if (!this.parkingLotOptions.SelectedValue.Any())
                    {
                        this.parkingLots = BLPurchaseTicket.GetParkingLots();

                        this.parkingLotOptions.DataTextField = "ShortName";
                        this.parkingLotOptions.DataValueField = "ID";
                        this.parkingLotOptions.DataSource = this.parkingLots;
                        this.parkingLotOptions.DataBind(); 

                        if (this.parkingLotOptions.SelectedValue.Any())
                        {
                            this.carparkTypeOptions.DataTextField = "CarparkType";
                            this.carparkTypeOptions.DataValueField = "CarparkTypeID";
                            IEnumerable<ParkingLot> selectedParkingLot = this.parkingLots.Where(parkingLot => parkingLot.ID == Convert.ToInt16(this.parkingLotOptions.SelectedValue));
                            this.carparkTypeOptions.DataSource = selectedParkingLot.First().CarparkTypes;
                            this.carparkTypeOptions.DataBind();
                        }
                    }
                }
            }             
        }
    }
}