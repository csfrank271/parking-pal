using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ParkingPal.DAL
{
    public class DALManagerDashboard
    {
        // Retrieves an appUser by searching for one with a matching userName and userPassword.
        public static List<InspectorUser> GetManagerInspectors(int managerID)
        {
            // Initialise appUser:
            List<InspectorUser> inspectorUsers = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_inspectors_for_manager", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@manager_id", SqlDbType.Int, 50).Value = managerID;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    if(reader.HasRows)
                    {
                        inspectorUsers = new List<InspectorUser>();
                        while (reader.Read())
                        {
                            AppUser appUser = new AppUser
                            (
                                (int)reader["AppUserID"],
                                reader["UserName"].ToString(),
                                reader["UserPassword"].ToString(),
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["AppUserType"].ToString().ToCharArray()[0]
                            );
                            Inspector inspector = new Inspector
                            (
                                (int)reader["InspectorID"],
                                (int)reader["AppUserID"],
                                (int)reader["ManagerID"]
                            );
                            inspectorUsers.Add(new InspectorUser(appUser, inspector));
                        }
                    }
                    
                    // Close the reader:
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return inspectorUsers;
        }
    }
}