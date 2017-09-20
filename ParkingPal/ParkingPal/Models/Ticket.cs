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
        public double Rate { get; set; }
        public string ParkingLotLocation { get; set; }
        public string CarparkType { get; set; }

        public Ticket(int ticketID, string rego, DateTime startDateTime, DateTime endDateTime,
            double rate, string parkingLotLocation, string carparkType)
        {
            this.TicketID = ticketID;
            this.Rego = rego;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.Rate = rate;
            this.ParkingLotLocation = parkingLotLocation;
            this.CarparkType = carparkType;
        }

    }
}