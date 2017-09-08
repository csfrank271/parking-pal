using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class InspectorUser
    {
        private AppUser appUser;
        private Inspector inspector;

        // Constructor
        public InspectorUser(AppUser appUser, Inspector inspector)
        {
            this.appUser = appUser;
            this.inspector = inspector;
        }

        // Properties
        public AppUser AppUser
        {
            get
            {
                return appUser;
            }

            set
            {
                appUser = value;
            }
        }

        public Inspector Inspector
        {
            get
            {
                return inspector;
            }

            set
            {
                inspector = value;
            }
        }
    }
}