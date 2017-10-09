using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLot
    {
        // Properties:
        private int parkingLotID, managerID;
        private int? adminID;
        private char approvalStatus;
        private string shortName, address, coordinates;
        private TimeSpan openTime, closeTime;

        public int ParkingLotID { get; set; }
        public int ManagerID { get; set; }
        public int? AdminID { get; set; }
        public char ApprovalStatus { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        // Constructor:
        public ParkingLot(int parkingLotID, int managerID, int? adminID,
            char approvalStatus, string shortName, string address,
            string coordinates, TimeSpan openTime, TimeSpan closeTime)
        {
            this.ParkingLotID = parkingLotID;
            this.ManagerID = managerID;
            this.AdminID = adminID;
            this.ApprovalStatus = approvalStatus;
            this.ShortName = shortName;
            this.Address = address;
            this.Coordinates = coordinates;
            this.OpenTime = openTime;
            this.CloseTime = closeTime;
        }
    }
}
