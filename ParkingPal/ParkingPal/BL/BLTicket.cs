using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.BL
{
    public class BLTicket
    {
        public static Ticket GetTicketInfo (int ticketId)
        {
            try
            {
                return DAL.DALTicket.GetTicket(ticketId).FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}