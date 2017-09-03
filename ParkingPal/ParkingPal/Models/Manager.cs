using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Manager
    {
        private int managerID, appUserID;
        private int? adminID;
        private char approvalStatus;

        public Manager(int managerID, int appUserID, int? adminID, char approvalStatus)
        {
            this.managerID = managerID;
            this.appUserID = appUserID;
            this.adminID = adminID;
            this.approvalStatus = approvalStatus;
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

        public int? AdminID
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

        public char ApprovalStatus
        {
            get
            {
                return approvalStatus;
            }

            set
            {
                approvalStatus = value;
            }
        }
    }
}