using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class Rate
    {
        public int RateId { get; set; }
        public int ParkingLotId { get; set; }
        public decimal TimeDuration { get; set; }
        public decimal HalfHourlyRate { get; set; }

        public Rate(int rateId, int parkingLotId, decimal timeDuraction, decimal halfHourlyRate)
        {
            this.RateId = rateId;
            this.ParkingLotId = parkingLotId;
            this.TimeDuration = timeDuraction;
            this.HalfHourlyRate = halfHourlyRate;

        }
    }
}
 
 