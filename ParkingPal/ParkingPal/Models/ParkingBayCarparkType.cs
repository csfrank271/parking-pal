using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingBayCarparkType
    {
        // Properties
        int carparkTypeID;
        string carparkType;
        int? parkingLotID, parkingBayCarparkTypeID, numberOfParks;
        public int CarparkTypeID { get; set; }
        public string CarparkType { get; set; }
        public int? ParkingLotID { get; set; }
        public int? ParkingBayCarparkTypeID { get; set; }
        public int? NumberOfParks { get; set; }

        // Constructor
        public ParkingBayCarparkType(int carparkTypeID, string carparkType,
            int? parkingLotID, int? parkingBayCarparkTypeID, int? numberOfParks)
        {
            this.CarparkTypeID = carparkTypeID;
            this.CarparkType = carparkType;
            this.ParkingLotID = parkingLotID;
            this.ParkingBayCarparkTypeID = parkingBayCarparkTypeID;
            this.NumberOfParks = numberOfParks;
        }
    }
}
