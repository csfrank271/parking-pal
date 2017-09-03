using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Ticket
    {
        private int ticketID;
        private string rego;
        private DateTime startDateTime, endDateTime;

        public Ticket(int ticketID, string rego, DateTime startDateTime, DateTime endDateTime)
        {
            this.ticketID = ticketID;
            this.rego = rego;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }

        public int TicketID
        {
            get
            {
                return ticketID;
            }

            set
            {
                ticketID = value;
            }
        }

        public string Rego
        {
            get
            {
                return rego;
            }

            set
            {
                rego = value;
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                return startDateTime;
            }

            set
            {
                startDateTime = value;
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                return endDateTime;
            }

            set
            {
                endDateTime = value;
            }
        }
    }
}