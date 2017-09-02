using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ParkingPal.DAL
{
    public class DALCommon
    {
        // Creates a new connection to the database:
        public static SqlConnection NewConnection()
        {
            return new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings
                ["parkingPalDBConnection"].ConnectionString);
        }
    }
}