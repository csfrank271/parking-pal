using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string Rego { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal Rate { get; set; }
        public string ParkingLotLocation { get; set; }
        public string CarparkType { get; set; }
        
        public Rate RateObject { get; set; }
        public ParkingLot ParkingLotObject { get; set; }
        public ParkingBay ParkingBayObject { get; set; }

        public List<Payment> Payments { get; set; }

        public string Email { get; set; }
        public Ticket(int ticketID, string rego, DateTime startDateTime, DateTime endDateTime,
            decimal rate, string parkingLotLocation, string carparkType,
            Rate rateObject, ParkingBay parkingBayObject, ParkingLot parkingLotObject, List<Payment> payments, string email)
        {
            this.TicketID = ticketID;
            this.Rego = rego;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.Rate = rate;
            this.ParkingLotLocation = parkingLotLocation;
            this.CarparkType = carparkType;
            this.RateObject = rateObject;
            this.ParkingBayObject = parkingBayObject;
            this.ParkingLotObject = parkingLotObject;
            this.Payments = payments;
            this.Email = email;
        }

    }
}