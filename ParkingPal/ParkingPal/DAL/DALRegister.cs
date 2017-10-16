using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingPal.Models;
using System.Data.SqlClient;
using System.Data;

namespace ParkingPal.DAL
{
    public class DALRegister
    {
        // Attempts to add an Inspector into the DB. Returns null if Inspector already exists.
        public static ManagerUser RegisterManager (string emailAddress, string password,
            string firstName, string lastName, char approvalStatus)
        {
            // Initialise appUser:
            ManagerUser managerUser = null;

            try
            {
                using (SqlConnection sqlConn = DALCommon.NewConnection())
                {
                    // Set the SQL command and its parameters
                    SqlCommand sqlComm = new SqlCommand("dbo.sp_create_manager", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@user_name", SqlDbType.VarChar, 50).Value = emailAddress;
                    sqlComm.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                    sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar, 50).Value = firstName;
                    sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar, 50).Value = lastName;
                    sqlComm.Parameters.Add("@approval_status", SqlDbType.Char, 1).Value = approvalStatus;
                    sqlComm.Parameters.Add("@new_manager_id", SqlDbType.Int);
                    sqlComm.Parameters["@new_manager_id"].Direction = ParameterDirection.Output;

                    // Open the SQL connection and run the command:
                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();

                    int newManagerID = (int)sqlComm.Parameters["@new_manager_id"].Value;
                    if (newManagerID != -1)
                    {
                        sqlComm = new SqlCommand("dbo.sp_get_manager_user", sqlConn);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        sqlComm.Parameters.Add("@manager_id", SqlDbType.Int).Value = newManagerID;
                        sqlComm.ExecuteNonQuery();
                        SqlDataReader reader = sqlComm.ExecuteReader();
                        // Retrieve the record if it exists:
                        while (reader.Read())
                        {
                            AppUser appUser = new AppUser
                            (
                                (int)reader["AppUserID"],
                                reader["UserName"].ToString(),
                                reader["UserPassword"].ToString(),
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                'M'
                            );
                            int? adminID = null;
                            object adminIDObj = reader["AdminID"];
                            if (adminIDObj != DBNull.Value)
                            {
                                adminID = (int)reader["AdminID"];
                            }
                            Manager manager = new Manager
                            (
                                (int)reader["ManagerID"],
                                (int)reader["AppUserID"],
                                adminID,
                                reader["ApprovalStatus"].ToString().ToCharArray()[0]
                            );
                            managerUser = new ManagerUser(appUser, manager);
                        }
                        // Close the reader:
                        reader.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return managerUser;
        }
    }
}