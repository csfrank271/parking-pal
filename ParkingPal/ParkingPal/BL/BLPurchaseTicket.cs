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


        public static InspectorUser AddInspector(int managerID, string userName, string password,
         string firstName, string lastName)
        {
            try
            {
                return DALManagerDashboard.AddInspector(managerID, userName, password, firstName, lastName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static int AddTicket(Ticket ticket)
        {
            try
            {
                return DALPurchaseTicket.AddTicket(ticket);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static Rate GetRate(DateTime startTime, DateTime endTime, int carparkLocation, string parkingBayType)
        {
            var span = endTime.Subtract(startTime).TotalMinutes;

            var timeDuration = Convert.ToDecimal(span);
            try
            {
                var rates = DALPurchaseTicket.GetRate(timeDuration, carparkLocation);
                Rate rate = null;
                decimal remainder = 0; 
                foreach (var r in rates)
                {
                    if ((rate == null || (timeDuration - r.TimeDuration) < remainder && (timeDuration -r.TimeDuration > 0)))
                    {
                        remainder = r.TimeDuration - timeDuration;
                        rate = r;
                    }

                }
                return rate;
            }
            catch (Exception expection)
            {
                throw expection;
            }
           // return timeDuration;
        }

        public static List<ParkingLot> GetParkingLots()
        {
            try
            {
                List<ParkingLot> parkingLots = new List<ParkingLot>();
                parkingLots = DALPurchaseTicket.GetParkingLots(); 
                List<ParkingBay> carparkTypes = new List<ParkingBay>();
                carparkTypes = DALPurchaseTicket.GetCarparkTypes(parkingLots);
                foreach(var parkingLot in parkingLots)
                {
                    parkingLot.CarparkTypes = new List<ParkingBay>();
                    foreach (var carparkType in carparkTypes)
                    {
                        if (parkingLot.ID == carparkType.ParkingLotID)
                        {
                            parkingLot.CarparkTypes.Add(carparkType);
                        }
                    }
                } 
                return parkingLots;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}