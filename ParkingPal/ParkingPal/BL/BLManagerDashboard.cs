using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;
using ParkingPal.Models;

namespace ParkingPal.BL
{
    public class BLManagerDashboard
    {
        public static List<InspectorUser> GetManagerInspectors(int managerID)
        {
            try
            {
                return DALManagerDashboard.GetManagerInspectors(managerID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteInspector(int inspectorID)
        {
            try
            {
                DALManagerDashboard.DeleteInspector(inspectorID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static void UpdateInspector(int inspectorID, string password, string firstName, string lastName)
        {
            try
            {
                DALManagerDashboard.UpdateInspector(inspectorID, password, firstName, lastName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
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

        public static List<ParkingLot> GetManagerParkingLots(int managerID)
        {
            try
            {
                return DALManagerDashboard.GetManagerParkingLots(managerID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateParkingLot(int parkingLotID, string shortName, string address,
            string coordinates, TimeSpan openTime, TimeSpan closeTime)
        {
            try
            {
                DALManagerDashboard.UpdateParkingLot(parkingLotID, shortName, address,
                    coordinates, openTime, closeTime);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static List<string> GetHalfHourIntervalsList()
        {
            List<string> halfHourIntervals = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                string hour = i.ToString();
                if (hour.Length == 1) hour = '0' + hour;

                halfHourIntervals.Add(hour + ":00");
                halfHourIntervals.Add(hour + ":30");
            }

            return halfHourIntervals;
        }

        public static ParkingLot AddParkingLot(int managerID, string name, string address,
            string coordinates, TimeSpan openTime, TimeSpan closeTime)
        {
            try
            {
                return DALManagerDashboard.AddParkingLot(managerID, name, address, coordinates, openTime, closeTime);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static List<ParkingBayCarparkType> GetParkingLotCarparkTypes(int parkingLotID)
        {
            try
            {
                return DALManagerDashboard.GetParkingLotCarparkTypes(parkingLotID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCarparkType(int parkingLotID, string carparkType, int parkCount)
        {
            try
            {
                DALManagerDashboard.UpdateCarparkType(parkingLotID, carparkType, parkCount);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static List<Rate> GetParkingLotRates(int parkingLotID)
        {
            try
            {
                return DALManagerDashboard.GetParkingLotRates(parkingLotID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int UpdateRate(int rateID, decimal timeDuration, decimal rateHourly)
        {
            try
            {
                return DALManagerDashboard.UpdateRate(rateID, timeDuration, rateHourly);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int AddParkingLotRate(int parkingLotID, decimal timeDuration, decimal rateHourly)
        {
            try
            {
                return DALManagerDashboard.AddParkingLotRate(parkingLotID, timeDuration, rateHourly);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Deletes a Rate:
        public static void DeleteParkingLotRate(int rateID)
        {
            try
            {
                DALManagerDashboard.DeleteParkingLotRate(rateID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}