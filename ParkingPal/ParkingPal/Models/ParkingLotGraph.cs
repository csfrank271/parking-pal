using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLotGraph
    {
        public string ParkingLotName { get; set; }
        public decimal PercentageFull { get; set; }

        public ParkingLotGraph (string ParkingLotName, decimal PercentageFull)
        {
            this.ParkingLotName = ParkingLotName;
            this.PercentageFull = PercentageFull;
        }
    }
}