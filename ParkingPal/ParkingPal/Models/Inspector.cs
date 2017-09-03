using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Inspector
    {
        private int inspectorID, appUserID, managerID;

        public Inspector(int inspectorID, int appUserID, int managerID)
        {
            this.InspectorID = inspectorID;
            this.AppUserID = appUserID;
            this.ManagerID = managerID;
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

        public int InspectorID
        {
            get
            {
                return inspectorID;
            }

            set
            {
                inspectorID = value;
            }
        }

        public int ManagerID
        {
            get
            {
                return managerID;
            }

            set
            {
                managerID = value;
            }
        }
    }
}