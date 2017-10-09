using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLot
    {
<<<<<<< HEAD
        public int ID { get; set; }
        public int? AdminID { get; set; }
        public int ManagerID { get; set; }
        public string ApprovalStatus { get; set; }
        public string ShortName { get; set; }
        public string LocationAddress { get; set; }
        public string Coordinates { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public List<ParkingBay> CarparkTypes { get; set; }
        public ParkingLot(int ID, int ManagerID, string ApprovalStatus, string ShortName, string LocationAddress, string Coordinates, string OpenTime, string CloseTime, int? AdminID, List<ParkingBay> CarparkTypes)
        {
            this.ID = ID;
            this.AdminID = AdminID;
            this.ManagerID = ManagerID;
            this.ApprovalStatus = ApprovalStatus;
            this.ShortName = ShortName;
            this.LocationAddress = LocationAddress;
            this.Coordinates = Coordinates;
            this.OpenTime = OpenTime;
            this.CloseTime = CloseTime;
            this.CarparkTypes = CarparkTypes;
=======
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
>>>>>>> cf_week10
        }
    }
}
