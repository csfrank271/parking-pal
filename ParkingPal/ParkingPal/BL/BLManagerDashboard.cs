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
        public static List<InspectorUser> GetManagerInspectors (int managerID)
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
        public static void UpdateInspector(int inspectorID, string firstName, string lastName)
        {
            try
            {
                DALManagerDashboard.UpdateInspector(inspectorID, firstName, lastName);
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
    }
}