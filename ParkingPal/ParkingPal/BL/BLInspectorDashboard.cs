using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;
using ParkingPal.Models;

namespace ParkingPal.BL
{
    public class BLInspectorDashboard
    {
        // Retrieves a list of non-expired Tickets associated with a registration number.
        public static List<Ticket> GetCurrentTicketsForRego(string rego)
        {
            try
            {
                return DALInspectorDashboard.GetCurrentTicketsForRego(rego);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}