using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class AppUser
    {
        private int appUserID;
        private string userName, userPassword, firstName,
            lasName;
        private char appUserType;

        public AppUser(int appUserID, string userName, string userPassword, string firstName, string lasName, char appUserType)
        {
            this.appUserID = appUserID;
            this.userName = userName;
            this.userPassword = userPassword;
            this.firstName = firstName;
            this.lasName = lasName;
            this.appUserType = appUserType;
        }

        public int AppUserID
        {
            get
            {
                return appUserID;
            }

            set
            {
                appUserID = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                userPassword = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LasName
        {
            get
            {
                return lasName;
            }

            set
            {
                lasName = value;
            }
        }

        public char AppUserType
        {
            get
            {
                return appUserType;
            }

            set
            {
                appUserType = value;
            }
        }
    }
}