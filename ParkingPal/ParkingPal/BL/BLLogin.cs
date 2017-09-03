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
    }
}
