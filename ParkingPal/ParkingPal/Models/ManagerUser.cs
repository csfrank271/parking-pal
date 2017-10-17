using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingPal.Models
{
    public class ManagerUser
    {
        private AppUser appUser;
        private Manager manager;

        public AppUser AppUser { get; set; }
        public Manager Manager { get; set; }

        public ManagerUser(AppUser appUser, Manager manager)
        {
            this.AppUser = appUser;
            this.Manager = manager;
        }
    }
}