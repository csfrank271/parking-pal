using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Payment
    {
        private int paymentID, ticketID;

        public Payment(int paymentID, int ticketID)
        {
            this.paymentID = paymentID;
            this.ticketID = ticketID;
        }

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
    }
}