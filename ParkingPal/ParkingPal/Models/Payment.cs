using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Payment
    {
        private int paymentID, ticketID;

        public int PaymentID
        {
            get
            {
                return paymentID;
            }

            set
            {
                paymentID = value;
            }
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

        public Payment (int paymentID, int ticketID)
        {
            PaymentID = paymentID;
            TicketID = ticketID;
        }
    }
}