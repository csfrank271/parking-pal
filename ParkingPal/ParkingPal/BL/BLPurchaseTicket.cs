using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;
using ParkingPal.Models;

namespace ParkingPal.BL
{
    public class BLPurchaseTicket
    {  
        public static List<Rate> GetRate(DateTime startTime, DateTime endTime, int carparkLocation, string parkingBayType)
        {
            var span = endTime.Subtract(startTime).TotalMinutes;

            var timeDuration = Convert.ToDecimal(span);
            try
            {
                return DALPurchaseTicket.GetRate(timeDuration, carparkLocation);
            }
            catch (Exception expection)
            {
                throw expection;
            }
           // return timeDuration;
        }
    }
}