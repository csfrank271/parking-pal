using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;
using ParkingPal.Models;
using System.Net.Mail;
using System.Net;

namespace ParkingPal.BL
{
    public class BLPurchaseTicket
    { 
        public static int AddTicket(Ticket ticket)
        {
            try
            {
                return DALPurchaseTicket.AddTicket(ticket);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int AddPayment(Payment payment, Ticket ticket)
        {
            try
            {
                int paymentAdded = DALPurchaseTicket.AddPayment(payment);


               using (MailMessage mm = new MailMessage("parkingpal9@gmail.com", ticket.Email))
                {
                    mm.Subject = "Confirmation Of Parking Ticket Purchase";
                    mm.Body = @"Thank you for purchasing a parking ticket with ParkingPal. <br />
                                  Please find below the details of your ticket and recent payment: <br />
                                  <table>
                                    <tr><td><b>Ticket ID:</b></td><td>"+payment.ticketID+@"</td></tr>
                                   <tr><td><b>Start</b></td><td>" + ticket.StartDateTime.ToString() + @"</td> 
                                  <td><b>End</b></td><td>" + ticket.EndDateTime.ToString() + @"</td></tr>
                                  <tr><td><b>Location</b></td><td>"+ticket.ParkingLotObject.ShortName+@"</td>
                                  <td><b>Type</b></td><td>"+ticket.ParkingBayObject.CarparkType+@"</td></tr>
                                  <tr><td><b>Rate</b></td><td>$"+ticket.Rate+@"</td>
                                  <td><b>Payment Total</b><td>$"+payment.total+@"</td></tr></table> <br />
                                  You are able to extend the time on all current tickets via the ParkingPal website using the ticket ID. 
                                    "; 
                    mm.IsBodyHtml = true; 
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("parkingpal9@gmail.com", "parkingpal1");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                } 
                    return paymentAdded;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Rate GetRate(DateTime startTime, DateTime endTime, int carparkLocation, string parkingBayType)
        {
            var span = endTime.Subtract(startTime).TotalMinutes;

            var timeDuration = Convert.ToDecimal(span);
            try
            {
                var rates = DALPurchaseTicket.GetRate(timeDuration, carparkLocation);
                Rate rate = null;
                decimal remainder = 0; 
                foreach (var r in rates)
                {
                    if ((rate == null || (timeDuration - r.TimeDuration) < remainder && (timeDuration -r.TimeDuration > 0)))
                    {
                        remainder = r.TimeDuration - timeDuration;
                        rate = r;
                    }

                }
                return rate;
            }
            catch (Exception expection)
            {
                throw expection;
            }
           // return timeDuration;
        }

        public static List<ParkingLot> GetParkingLots()
        {
            try
            {
                List<ParkingLot> parkingLots = new List<ParkingLot>();
                parkingLots = DALPurchaseTicket.GetParkingLots(); 
                List<ParkingBay> carparkTypes = new List<ParkingBay>();
                carparkTypes = DALPurchaseTicket.GetCarparkTypes(parkingLots);
                foreach(var parkingLot in parkingLots)
                {
                    parkingLot.CarparkTypes = new List<ParkingBay>();
                    foreach (var carparkType in carparkTypes)
                    {
                        if (parkingLot.ID == carparkType.ParkingLotID)
                        {
                            parkingLot.CarparkTypes.Add(carparkType);
                        }
                    }
                } 
                return parkingLots;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}