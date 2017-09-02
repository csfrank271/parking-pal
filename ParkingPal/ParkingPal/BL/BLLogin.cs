using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.DAL;

namespace ParkingPal.BL
{
    public class BLLogin
    {
        public static char GetAppUserType (string userName,
            string password)
        {
            return DAL.DALLogin.GetAppUserType(userName, password);
        }
    }
}