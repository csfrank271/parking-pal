using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.UL
{
    public partial class ULRetrieveTicketInfo : System.Web.UI.Page
    {
        Ticket ticket;
        TimeSpan carparkEndTime;
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
                } else if (!IsPostBack)
                {
                    this.ticket = (Ticket)Session["ExtendTicket"];
                    if (this.ticket == null)
                    {
                        var strUrl = "~/UL/ULHome.aspx";
                        Response.Redirect(strUrl);
                    } else
                    {
                        this.spanTotalPrice.InnerText = (this.ticket.Rate).ToString();
                        this.divStartTime.InnerText = this.ticket.StartDateTime.ToShortTimeString();
                        this.divEndTime.InnerText = this.ticket.EndDateTime.ToShortTimeString();
                        this.divRego.InnerText = this.ticket.Rego;
                        this.divLocation.InnerText = BL.BLTicket.GetLocation(Convert.ToInt16(this.ticket.ParkingLotLocation));
                        this.divType.InnerText = BL.BLTicket.GetType(Convert.ToInt16(this.ticket.CarparkType), Convert.ToInt16(this.ticket.ParkingLotLocation));
                        this.carparkEndTime = BL.BLTicket.GetParkingLotEndTime(Convert.ToInt16(this.ticket.ParkingLotLocation));
                        var time = this.ticket.EndDateTime.TimeOfDay;
                        List<TimeSpan> times = new List<TimeSpan>();
                        TimeSpan add = new TimeSpan(0, 30, 0);
                        while (time < this.carparkEndTime)
                        {
                            time = time.Add(add);
                            if (time > this.carparkEndTime) { times.Add(this.carparkEndTime); } else { times.Add(time); }
                        }
                        if (!times.Any())
                        {
                            var strUrl = "~/UL/ULUnableToExtend.aspx";
                            Response.Redirect(strUrl);
                        }
                        this.selectExtendTill.DataSource = times;
                        this.selectExtendTill.DataBind();
                        Session["EndTime"] = this.selectExtendTill.SelectedValue;
                        TimeSpan newEndTime = Convert.ToDateTime(this.selectExtendTill.SelectedValue).TimeOfDay;
                        double difference = (newEndTime - this.ticket.EndDateTime.TimeOfDay).Minutes;
                        this.totalCost.InnerText = ((Convert.ToInt16(this.ticket.Rate)) * (difference / 30)).ToString();
                    }
                }
            }
        } 

        [WebMethod]
        protected void ticketEndTimeChanged(object sender, EventArgs e)
        {
            this.ticket = (Ticket)Session["ExtendTicket"];
            TimeSpan newEndTime = Convert.ToDateTime(this.selectExtendTill.SelectedValue).TimeOfDay;
            double difference = (newEndTime - this.ticket.EndDateTime.TimeOfDay).TotalMinutes;
            this.totalCost.InnerText = ((Convert.ToDouble(this.ticket.Rate)) * (difference / 30)).ToString();
            Session["EndTime"] = this.selectExtendTill.SelectedValue;
            Session["PaymentCost"] = this.totalCost.InnerText;
        } 
    }
}