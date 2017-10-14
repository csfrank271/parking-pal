using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Payment
    { 
        public int paymentID { get; set; }
        public int ticketID { get; set; }
        public decimal total { get; set; }

        public string status { get; set; }

        public Payment(int paymentID, int ticketID, decimal total, string status)
        {
            this.paymentID = paymentID;
            this.ticketID = ticketID;
            this.total = total;
            this.status = status;
        } 
    }
}