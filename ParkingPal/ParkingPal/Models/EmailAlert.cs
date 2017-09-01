using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class EmailAlert
    {
        private int emailAlertID, ticketID;
        private string emailAddress, alertMessage;
        private DateTime alertTime;

        public int EmailAlertID
        {
            get
            {
                return emailAlertID;
            }

            set
            {
                emailAlertID = value;
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

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                emailAddress = value;
            }
        }

        public string AlertMessage
        {
            get
            {
                return alertMessage;
            }

            set
            {
                alertMessage = value;
            }
        }

        public DateTime AlertTime
        {
            get
            {
                return alertTime;
            }

            set
            {
                alertTime = value;
            }
        }

        public EmailAlert (int emailAlertID, int ticketID, string emailAddress,
            string alertMessage, DateTime alertTime)
        {
            EmailAlertID = emailAlertID;
            TicketID = ticketID;
            EmailAddress = emailAddress;
            AlertMessage = alertMessage;
            AlertTime = alertTime;
        }
    }
}