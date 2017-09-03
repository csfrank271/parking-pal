using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ParkingPal.Models;

namespace ParkingPal.DAL
{
    public class DALLogin
    {
        // Retrieves an appUser by searching for one with a matching userName and userPassword.
        public static AppUser GetAppUser(string userName,
            string password)
        {
            // Initialise appUser:
            AppUser appUser = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_user_type", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@user_name", SqlDbType.VarChar, 50).Value = userName;
                    sqlComm.Parameters.Add("@user_password", SqlDbType.VarChar, 50).Value = password;
                    
                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    while (reader.Read())
                    {
                        appUser = new AppUser
                        (
                            (int)reader["ID"],
                            reader["UserName"].ToString(),
                            reader["UserPassword"].ToString(),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["AppUserType"].ToString().ToCharArray()[0]
                        );
                    }
                    // Close the reader:
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
            return appUser;
        }

        // Retrieves a Manager by searching for one with a matching appUserID.
        public static Manager GetManager(int appUserID)
        {
            // Initialise manager:
            Manager manager = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_get_manager", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@user_id", SqlDbType.Int).Value = appUserID;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    // Retrieve the record if it exists:
                    while (reader.Read())
                    {
                        int? adminID = null;
                        var adminID_db = reader["adminID"];
                        if (!Convert.IsDBNull(adminID_db))
                        {
                            adminID = adminID_db as int?;
                        }

                        manager = new Manager
                        (
                            (int)reader["ID"],
                            (int)reader["AppUserID"],
                            adminID,
                            reader["ApprovalStatus"].ToString().ToCharArray()[0]
                        );
                    }
                    // Close the reader:
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return manager;
        }
    }
}