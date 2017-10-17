using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using ParkingPal.DAL;

namespace ParkingPal.BL
{
    public class BLRegister
    {
        public static ManagerUser RegisterManager(string emailAddress, string password,
            string firstName, string lastName, char approvalStatus)
        {
            return DALRegister.RegisterManager(emailAddress, password,
                firstName, lastName, approvalStatus);
        }
    }
}