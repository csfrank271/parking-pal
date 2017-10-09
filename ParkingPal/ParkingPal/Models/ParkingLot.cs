using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLot
    {
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
        }
    }
}