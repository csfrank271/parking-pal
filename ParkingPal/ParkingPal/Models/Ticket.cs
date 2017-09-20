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
        public float Rate { get; set; }
        public string ParkingLotShortName { get; set; }
        //public ParkingLot ParkingLotLocation { get; set; }
        //public string CarparkType { get; set; } // change to CarparkType when that model is added 

        public Ticket(int ticketID, string rego, DateTime startDateTime, DateTime endDateTime,
            float rate, string parkingLotShortName)
        {
            this.TicketID = ticketID;
            this.Rego = rego;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.Rate = rate;
            this.ParkingLotShortName = parkingLotShortName;
            // this.ParkingLotLocation = ParkingLotLocation;
            // this.CarparkType = carparkType;
        }

    }
}