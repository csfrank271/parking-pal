using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingBay
    {
        public int ParkingLotID { get; set; }
        public int CarparkTypeID { get; set; }
        public int NumberOfParks { get; set; }

        public string CarparkType { get; set; }
        public ParkingBay(int ParkingLotID, int CarparkTypeID, int NumberOfParks, string CarparkType)
        {
            this.NumberOfParks = NumberOfParks;
            this.ParkingLotID = ParkingLotID;
            this.CarparkTypeID = CarparkTypeID;
            this.CarparkType = CarparkType; 
        }
    }
}

 