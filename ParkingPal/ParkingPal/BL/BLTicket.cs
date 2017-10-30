using ParkingPal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.BL
{
    public class BLTicket
    {
        public static int GetUsedParkingLots(int parkingLotId)
        {
            try
            {
                return DAL.DALTicket.GetUsedParkingLots(parkingLotId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int GetTotalParkingLots(int parkingLotId)
        {
            try
            {
                return DAL.DALTicket.GetTotalParkingLots(parkingLotId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Ticket UpdateTicket(Ticket ticket)
        {
            try
            {
                return DAL.DALTicket.UpdateTicket(ticket);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static List<Ticket> GetTicketInfo(int ticketId)
        {
            try
            {
                return DAL.DALTicket.GetTicket(ticketId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static string GetLocation(int locationId)
        {
            try
            {
                return DAL.DALTicket.GetLocation(locationId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static TimeSpan GetParkingLotEndTime(int locationId)
        {
            try
            {
                return DAL.DALTicket.GetParkingLotEndTime(locationId);
            } catch (Exception exception)
            {
                throw exception;
            }
        }
        public static string GetType (int typeId, int parkingLotId)
        {
            try
            {
                return DAL.DALTicket.GetType(typeId, parkingLotId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}