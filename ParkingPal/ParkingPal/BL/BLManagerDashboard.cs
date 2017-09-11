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
    }
}