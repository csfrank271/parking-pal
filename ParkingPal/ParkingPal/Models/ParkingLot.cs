using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ParkingLot
    {
        private int parkingLotID, adminID, totalCarparks, functioningCarparks;
        private char approvalStatus;
        private string address, coordinates;

        public int ParkingLotID
        {
            get
            {
                return this.parkingLotID;
            }
            set
            {
                this.parkingLotID = value;
            }
        }

        public int AdminID
        {
            get
            {
                return this.adminID;
            }
            set
            {
                this.adminID = value;
            }
        }

        public int TotalCarparks
        {
            get
            {
                return this.totalCarparks;
            }
            set
            {
                this.totalCarparks = value;
            }
        }
        public int FunctioningCarparks
        {
            get
            {
                return this.functioningCarparks;
            }
            set
            {
                this.functioningCarparks = value;
            }
        }
        public char ApprovalStatus
        {
            get
            {
                return this.approvalStatus;
            }
            set
            {
                this.approvalStatus = value;
            }
        }
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }
        public string Coordinates
        {
            get
            {
                return this.coordinates;
            }
            set
            {
                this.coordinates = value;
            }
        }

        public ParkingLot(int parkingLotID, int adminID, int totalCarparks,
            int functioningCarparks, char approvalStatus, string address,
            string coordinates)
        {
            ParkingLotID = parkingLotID;
            AdminID = adminID;
            TotalCarparks = totalCarparks;
            FunctioningCarparks = functioningCarparks;
            ApprovalStatus = approvalStatus;
            Address = address;
            Coordinates = coordinates;
        }
    }
}