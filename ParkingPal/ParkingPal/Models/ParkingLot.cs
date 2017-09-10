using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLot
    {
        private int parkingLotID, totalCarparks, generalParkingSpots,
            disabledParkingSpots, carpoolParkingSpots, serviceParkingSpots,
            positionParkingSpots, reservedParkingSpots, visitorsParkingSpots;
        private int? adminID;
        private char approvalStatus;
        private string shortName, address, coordinates;

        public int ParkingLotID
        {
            get
            {
                return parkingLotID;
            }

            set
            {
                parkingLotID = value;
            }
        }

        public int TotalCarparks
        {
            get
            {
                return totalCarparks;
            }

            set
            {
                totalCarparks = value;
            }
        }

        public int GeneralParkingSpots
        {
            get
            {
                return generalParkingSpots;
            }

            set
            {
                generalParkingSpots = value;
            }
        }

        public int DisabledParkingSpots
        {
            get
            {
                return disabledParkingSpots;
            }

            set
            {
                disabledParkingSpots = value;
            }
        }

        public int CarpoolParkingSpots
        {
            get
            {
                return carpoolParkingSpots;
            }

            set
            {
                carpoolParkingSpots = value;
            }
        }

        public int ServiceParkingSpots
        {
            get
            {
                return serviceParkingSpots;
            }

            set
            {
                serviceParkingSpots = value;
            }
        }

        public int PositionParkingSpots
        {
            get
            {
                return positionParkingSpots;
            }

            set
            {
                positionParkingSpots = value;
            }
        }

        public int ReservedParkingSpots
        {
            get
            {
                return reservedParkingSpots;
            }

            set
            {
                reservedParkingSpots = value;
            }
        }

        public int VisitorsParkingSpots
        {
            get
            {
                return visitorsParkingSpots;
            }

            set
            {
                visitorsParkingSpots = value;
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

        public string ShortName
        {
            get
            {
                return shortName;
            }

            set
            {
                shortName = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Coordinates
        {
            get
            {
                return coordinates;
            }

            set
            {
                coordinates = value;
            }
        }

        public ParkingLot(int parkingLotID, int totalCarparks,
            int generalParkingSpots, int disabledParkingSpots,
            int carpoolParkingSpots, int serviceParkingSpots,
            int positionParkingSpots, int reservedParkingSpots,
            int visitorsParkingSpots, int? adminID, char approvalStatus,
            string shortName, string address, string coordinates)
        {
            this.ParkingLotID = parkingLotID;
            this.TotalCarparks = totalCarparks;
            this.GeneralParkingSpots = generalParkingSpots;
            this.DisabledParkingSpots = disabledParkingSpots;
            this.CarpoolParkingSpots = carpoolParkingSpots;
            this.ServiceParkingSpots = serviceParkingSpots;
            this.PositionParkingSpots = positionParkingSpots;
            this.ReservedParkingSpots = reservedParkingSpots;
            this.VisitorsParkingSpots = visitorsParkingSpots;
            this.AdminID = adminID;
            this.ApprovalStatus = approvalStatus;
            this.ShortName = shortName;
            this.Address = address;
            this.Coordinates = coordinates;
        }
    }
}