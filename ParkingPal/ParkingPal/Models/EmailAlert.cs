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

        public EmailAlert(int emailAlertID, int ticketID, string emailAddress, string alertMessage, DateTime alertTime)
        {
            this.emailAlertID = emailAlertID;
            this.ticketID = ticketID;
            this.emailAddress = emailAddress;
            this.alertMessage = alertMessage;
            this.alertTime = alertTime;
        }

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
    }
}