using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;
using ParkingPal.Models;

namespace ParkingPal.BL
{
    public class BLLogin
    {
        public static AppUser GetAppUser (string userName,
            string password)
        {
            try
            {
                return DALLogin.GetAppUser(userName, password);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Manager GetManager(int appUserID)
        {
            try
            {
                return DALLogin.GetManager(appUserID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Inspector GetInspector(int appUserID)
        {
            try
            {
                return DALLogin.GetInspector(appUserID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Administrator GetAdministrator(int appUserID)
        {
            try
            {
                return DALLogin.GetAdministrator(appUserID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
