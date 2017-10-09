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
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        public List<ParkingBay> CarparkTypes { get; set; }

        public ParkingLot(int id, int? adminID, int managerID, string approvalStatus, string shortName,
            string locationAddress, string coordinates, TimeSpan openTime, TimeSpan closeTime,
            List<ParkingBay> carparkTypes)
        {
            this.ID = id;
            this.AdminID = adminID;
            this.ManagerID = managerID;
            this.ApprovalStatus = approvalStatus;
            this.ShortName = shortName;
            this.LocationAddress = locationAddress;
            this.Coordinates = coordinates;
            this.OpenTime = openTime;
            this.CloseTime = closeTime;
            this.CarparkTypes = carparkTypes;
        }
    }
}
