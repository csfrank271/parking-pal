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
        public ParkingLot ParkingLotLocation { get; set; }
        public string CarparkType { get; set; } // change to CarparkType when that model is added 
        public Ticket(int ticketID, string rego, DateTime startDateTime, DateTime endDateTime, ParkingLot parkingLotLocation, string carparkType)
        {
            this.TicketID = ticketID;
            this.Rego = rego;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.ParkingLotLocation = parkingLotLocation;
            this.CarparkType = carparkType;
        }

    }
}