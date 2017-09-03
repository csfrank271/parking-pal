using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Administrator
    {
        private int adminID, appUserID;

        public Administrator(int adminID, int appUserID)
        {
            this.AdminID = adminID;
            this.AppUserID = appUserID;
        }

        public int AdminID
        {
            get
            {
                return adminID;
            }

            set
            {
                adminID = value;
            }
        }

        public int AppUserID
        {
            get
            {
                return appUserID;
            }

            set
            {
                appUserID = value;
            }
        }
    }
}